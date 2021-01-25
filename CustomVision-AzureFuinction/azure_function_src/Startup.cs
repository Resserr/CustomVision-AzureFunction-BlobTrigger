using System;
using System.Reflection;
using CustomVisionLibraryApproach.Interfaces;
using CustomVisionLibraryApproach.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CustomVisionLibraryApproach.Startup))]
namespace CustomVisionLibraryApproach
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfigurationRoot config = GenerateConfigurationObject();

            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.AddSingleton<ICustomVisionTrainingClientWrapper, CustomVisionTrainingClientWrapper>();
            builder.Services.AddSingleton<ICustomVisionPredictionClientWrapper, CustomVisionPredictionClientWrapper>();
            builder.Services.AddTransient<ICustomVisionTagsHelper, CustomVisionTagsHelper>();
            builder.Services.AddTransient<IFileNameParser, FileNameParser>();
        }

        private IConfigurationRoot GenerateConfigurationObject()
        {
            return new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory)
               .AddEnvironmentVariables() // add Environment Variables provider
               .AddJsonFile("local.settings.json", true) // add Json Provider(local.settings.json)
               .AddUserSecrets(Assembly.GetExecutingAssembly(), false) // add user secrets
               .Build();
        }
    }
}