using Microsoft.Extensions.Configuration;
using NobelPrizeApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NobelPrizeApp.Configuration
{
    public static class AppConfiguration
    {
        private static string appSettingsPath;
        private static string jsonFile;
        private static ApplicationSettings appSettings;

        public static void ReadConfiguration()
        {
            appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            jsonFile = File.ReadAllText(appSettingsPath);

            appSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationSettings>(jsonFile);
        }

        public static void WriteConfiguration()
        {
            jsonFile = Newtonsoft.Json.JsonConvert.SerializeObject(appSettings);

            File.WriteAllText(appSettingsPath, jsonFile);
        }

        public static void UpdateLastExecution()
        {
            appSettings.LastExecution = DateTime.Now.ToString();
        }

        public static DateTime GetLastExecution()
        {
            DateTime dtReturn;

            if (!DateTime.TryParse(appSettings.LastExecution, out dtReturn))
                dtReturn = DateTime.MinValue;

            return dtReturn;
        }

    }
}
