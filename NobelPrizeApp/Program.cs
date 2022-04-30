using Microsoft.Extensions.Configuration;
using NLog;
using NobelPrizeApp.APIRetrieve;
using NobelPrizeApp.Configuration;
using NobelPrizeApp.FileReader;
using NobelPrizeApp.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NobelPrizeApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IFileReader fReader;

            string pathFile = args[0];

            fReader = new FileReader.FileReader();

            if (!String.IsNullOrEmpty(fReader.ValidatePath(pathFile)))
            {
                Console.Write("Please input the File Path: ");
                pathFile = Console.ReadLine();
            }

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            AppConfiguration.ReadConfiguration();

            APIInformationRetriever apiRetriever = new APIInformationRetriever();

            FileInformation<NobelPrizeRequest> infos = new FileInformation<NobelPrizeRequest>();
            
            infos = fReader.ReadFile(pathFile);

            await APIInformationRetriever.NobelPrizeRetrieveInformation(infos.file);


            AppConfiguration.UpdateLastExecution();
            AppConfiguration.WriteConfiguration();

            System.Threading.Thread.Sleep(5000);
        }
    }
}
