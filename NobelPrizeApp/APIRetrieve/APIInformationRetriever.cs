using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NobelPrizeApp.ApplicationLogger;
using NobelPrizeApp.Configuration;
using NobelPrizeApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NobelPrizeApp.APIRetrieve
{
    public class APIInformationRetriever
    {
        static HttpClient client = new HttpClient();
        static DateTime lastCall = new DateTime();
        static TimeSpan ts;
        static int sToWait = 0;

        public APIInformationRetriever()
        {
            client.BaseAddress = new Uri("https://api.nobelprize.org/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task NobelPrizeRetrieveInformation(NobelPrizeRequest requests)
        {
            FileInformation<List<NobelPrizeInformation>> returnObject = new FileInformation<List<NobelPrizeInformation>>();
            returnObject.file = new List<NobelPrizeInformation>();

            returnObject.errorMessage = await ValidateRequests(requests);

            NobelPrizeInformation test = new NobelPrizeInformation();

            lastCall = AppConfiguration.GetLastExecution();

            ts = DateTime.Now.Subtract(lastCall);
            sToWait = ts.TotalSeconds >= 60 ? 0 : 60 - ts.Seconds;

            System.Threading.Thread.Sleep(sToWait * 1000);

            foreach (RequestInformation req in requests.requests)
            {
                ts = DateTime.Now.Subtract(lastCall);

                sToWait = ts.TotalSeconds >= 15 ? 0 : 15 - ts.Seconds;

                System.Threading.Thread.Sleep(sToWait * 1000);

                test = await GetNobels(req);
                
                returnObject.file.Add(test);
            }

        }

        static async Task<NobelPrizeInformation> GetNobels(RequestInformation reqInformation)
        {

            AppLogger.LogPrizeRequest(reqInformation);

            NobelPrizeInformation retObject = null;

            HttpResponseMessage response = await client.GetAsync($"prize.json?category={reqInformation.category}&year={reqInformation.year}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                retObject = JsonConvert.DeserializeObject<NobelPrizeInformation>(apiResponse);

                AppLogger.LogPrizeInformation(retObject);
            }
            else
            {
                AppLogger.LogPrizeError(response.ReasonPhrase);
            }

            lastCall = DateTime.Now;

            return retObject;
        }

        private static Task<string> ValidateRequests(NobelPrizeRequest requests)
        {
            if (requests is null)
                return Task.FromResult("The requests parameter is null");

            if (requests.requests is null || requests.requests.Length <= 0)
                return Task.FromResult("There are no requests to process.");

            return Task.FromResult(String.Empty);
        }
    }
}
