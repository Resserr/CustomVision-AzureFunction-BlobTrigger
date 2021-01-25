using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace CustomVisionLibraryApproach.Interfaces
{
    public interface ICustomVisionTrainingClientWrapper
    {
        Task<Project> GetProjectAsync(Guid projectId);
        Task<IList<Tag>> GetTagsAsync(Guid projectId);
        Task<ImageCreateSummary> CreateImageFromDataAsync(Guid projectId, Stream stream, List<Guid> tags);
    }
}