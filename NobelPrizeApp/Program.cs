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
            string pathFile;

            fReader = new FileReader.FileReader();

            if (args.Length > 0)
            {
                pathFile = args[0];
            }
            else
            {
                Console.Write("Please input the File Path: ");
                pathFile = Console.ReadLine();
            }

            if (!String.IsNullOrEmpty(fReader.ValidatePath(pathFile)))
            {
                Console.Write("The path informed is invalid.");

                return;
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
