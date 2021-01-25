using CustomVisionLibraryApproach.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Extensions.Configuration;

namespace CustomVisionLibraryApproach.Services
{
    public class CustomVisionPredictionClientWrapper : ICustomVisionPredictionClientWrapper
    {
        private CustomVisionPredictionClient _customVisionPredictionClient;
        public CustomVisionPredictionClientWrapper(IConfiguration config)
        {
            _customVisionPredictionClient = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(config["CUSTOM_VISION_PREDICTION_KEY"]))
            {
                Endpoint = config["CUSTOM_VISION_PREDICTION_ENDPOINT"]
            };
        }
    }
}