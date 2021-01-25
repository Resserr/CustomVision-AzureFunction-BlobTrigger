using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomVisionLibraryApproach.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CustomVisionLibraryApproach
{
    public class CustomVisionLibraryApproach
    {
        private ICustomVisionPredictionClientWrapper _customVisionPredictionClientWrapper;
        private ICustomVisionTrainingClientWrapper _customVisionTrainingClientWrapper;
        private IConfiguration _configuration;
        private ICustomVisionTagsHelper _customVisionTagsHelper;
        private IFileNameParser _fileNameParser;
        public CustomVisionLibraryApproach(ICustomVisionTrainingClientWrapper customVisionTrainingClient, 
                                                ICustomVisionPredictionClientWrapper customVisionPredictionClient,
                                                    IConfiguration configuration, ICustomVisionTagsHelper customVisionTagsHelper,
                                                        IFileNameParser fileNameParser)
        {
            _customVisionPredictionClientWrapper = customVisionPredictionClient;
            _customVisionTrainingClientWrapper = customVisionTrainingClient;
            _configuration = configuration;
            _customVisionTagsHelper = customVisionTagsHelper;
            _fileNameParser = fileNameParser;
        }
        [FunctionName("AIBlobFunction")]
        public async Task Run([BlobTrigger("image-store-container/{name}", Connection = "ConnectionString")]Stream image, string name, ILogger log)
        {
            Guid projectId = new Guid(_configuration["ProjectId"]);

            Project project = await _customVisionTrainingClientWrapper.GetProjectAsync(projectId);
            if(project == null)
            {
                log.LogError("Can't find proper project. Check your ProjectId configuration inside appsettings");
                return;
            }
            IList<Tag> availableTags = await _customVisionTrainingClientWrapper.GetTagsAsync(projectId);
            if(!availableTags.Any())
            {
                log.LogError("No tags for the current project. Please, add tag first and then upload your photo again.");
                return;
            }

            string fileName = _fileNameParser.ParseFileName(name);
            
            Tag properTag = _customVisionTagsHelper.GetTagBasedOnFileName(availableTags, fileName);
            if(properTag == null)
            {
                log.LogError("Can't find proper tag for the current image with name:" + name);
                return;
            }

            ImageCreateSummary result = await _customVisionTrainingClientWrapper.CreateImageFromDataAsync(projectId, image, new List<Guid>() { properTag.Id });
            if(!result.IsBatchSuccessful)
            {
                log.LogError("Can't add this photo to the Custom Vision project");
                return;
            }
                
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {image.Length} Bytes");
        }
    }
}
