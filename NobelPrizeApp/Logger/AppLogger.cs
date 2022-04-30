using Newtonsoft.Json;
using NLog;
using NobelPrizeApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.AppLogger
{
    public static class AppLogger
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogPrizeInformation(NobelPrizeInformation prizeInformation)
        {
            Logger.Error(JsonConvert.SerializeObject(prizeInformation.prizes));
        }

    }
}
