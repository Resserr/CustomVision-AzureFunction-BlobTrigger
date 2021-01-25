using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace CustomVisionLibraryApproach.Interfaces
{
    public interface ICustomVisionTagsHelper
    {
        Tag GetTagBasedOnFileName(IList<Tag> tags, string fileName);
    }
}