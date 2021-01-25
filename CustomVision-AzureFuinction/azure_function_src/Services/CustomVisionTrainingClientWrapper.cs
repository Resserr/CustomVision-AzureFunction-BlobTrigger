using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CustomVisionLibraryApproach.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using Microsoft.Extensions.Configuration;

namespace CustomVisionLibraryApproach.Services
{
    public class CustomVisionTrainingClientWrapper : ICustomVisionTrainingClientWrapper
    {
        private CustomVisionTrainingClient _customVisionTrainingClient;
        public CustomVisionTrainingClientWrapper(IConfiguration config)
        {
            _customVisionTrainingClient = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(config["CUSTOM_VISION_TRAINING_KEY"]))
            {
                Endpoint = config["CUSTOM_VISION_TRAINING_ENDPOINT"]
            };
        }
        public async Task<ImageCreateSummary> CreateImageFromDataAsync(Guid projectId, Stream stream, List<Guid> tags)
        {
            return await _customVisionTrainingClient.CreateImagesFromDataAsync(projectId, stream, tags);
        }
        public async Task<Project> GetProjectAsync(Guid projectId)
        {
            return await _customVisionTrainingClient.GetProjectAsync(projectId);
        }
        public async Task<IList<Tag>> GetTagsAsync(Guid projectId)
        {
            return await _customVisionTrainingClient.GetTagsAsync(projectId);
        }
    }
}