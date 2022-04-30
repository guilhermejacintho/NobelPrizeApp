using Newtonsoft.Json;
using NLog;
using NobelPrizeApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.ApplicationLogger
{
    public static class AppLogger
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogPrizeInformation(NobelPrizeInformation prizeInformation)
        {
            Logger.Info(String.Concat("API Answer: ", JsonConvert.SerializeObject(prizeInformation.prizes)));
        }

        public static void LogPrizeRequest(RequestInformation prizeRequest)
        {
            Logger.Trace($"Requesting Nobel Prizes for {prizeRequest.category} on the year {prizeRequest.year}.");
            Logger.Info($"Requesting Nobel Prizes for {prizeRequest.category} on the year {prizeRequest.year}.");
            Logger.Trace($"URL: https://api.nobelprize.org/v1/prize.json?category={prizeRequest.category}&year={prizeRequest.year}");
        }

        public static void LogPrizeError(string errorMessage)
        {
            Logger.Info("An error has ocurred on retrieving prize information.");
            Logger.Error(errorMessage);
        }



    }
}
