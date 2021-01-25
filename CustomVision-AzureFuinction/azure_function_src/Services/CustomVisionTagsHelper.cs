using System.Collections.Generic;
using System.Linq;
using CustomVisionLibraryApproach.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace CustomVisionLibraryApproach.Services
{
    public class CustomVisionTagsHelper : ICustomVisionTagsHelper
    {
        public Tag GetTagBasedOnFileName(IList<Tag> tags, string fileName)
        {
            return tags.FirstOrDefault(tag => tag.Name == fileName );
        }
    }
}