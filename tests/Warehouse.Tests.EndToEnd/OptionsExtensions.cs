﻿using Microsoft.Extensions.Configuration;

namespace Warehouse.Tests.EndToEnd
{
    public class OptionsExtensions
    {
        public static TSettings GetOptions<TSettings>(string section, string settingsFileName = null) 
            where TSettings : class, new()
        {
            settingsFileName ??= "appsettings.tests.json";
            var configuration = new TSettings();

            GetConfigurationRoot(settingsFileName)
                .GetSection(section)
                .Bind(configuration);

            return configuration;
        }

        private static IConfigurationRoot GetConfigurationRoot(string settingsFileName)
            => new ConfigurationBuilder()
                .AddJsonFile(settingsFileName, optional: true)
                .AddEnvironmentVariables()
                .Build();
    }
}
