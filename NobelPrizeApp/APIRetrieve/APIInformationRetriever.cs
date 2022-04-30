using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NobelPrizeApp.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NobelPrizeApp.APIRetrieve
{
    public class APIInformationRetriever
    {
        static HttpClient client = new HttpClient();

        public APIInformationRetriever()
        {
            client.BaseAddress = new Uri("https://api.nobelprize.org/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<FileInformation<List<NobelPrizeInformation>>> NobelPrizeRetrieveInformation(NobelPrizeRequest requests)
        {
            FileInformation<List<NobelPrizeInformation>> returnObject = new FileInformation<List<NobelPrizeInformation>>();
            returnObject.file = new List<NobelPrizeInformation>();

            returnObject.errorMessage = await ValidateRequests(requests);

            NobelPrizeInformation test = new NobelPrizeInformation();

            foreach (RequestInformation req in requests.requests)
            {
                test = await GetNobels(req);
                returnObject.file.Add(test);
            }

            return returnObject;
        }

        static async Task<NobelPrizeInformation> GetNobels(RequestInformation reqInformation)
        {
            NobelPrizeInformation retObject = null;

            HttpResponseMessage response = await client.GetAsync($"prize.json?category={reqInformation.category}&year={reqInformation.year}");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                retObject = JsonConvert.DeserializeObject<NobelPrizeInformation>(apiResponse);
            }

            return retObject;
        }

        private static async Task<string> ValidateRequests(NobelPrizeRequest requests)
        {
            if (requests is null)
                return "The requests parameter is null";

            if (requests.requests is null || requests.requests.Length <= 0)
                return "There are no requests to process.";

            return String.Empty;
        }
    }
}
