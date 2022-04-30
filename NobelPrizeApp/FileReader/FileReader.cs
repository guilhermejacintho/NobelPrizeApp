using NobelPrizeApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.FileReader
{
    public class FileReader : IFileReader
    {

        public FileInformation<NobelPrizeRequest> ReadFile(string path)
        {

            FileInformation<NobelPrizeRequest> returnObject  = new FileInformation<NobelPrizeRequest>();
            NobelPrizeRequest requestReturn = new NobelPrizeRequest();
            List<RequestInformation> reqInformations = new List<RequestInformation>();

            returnObject.errorMessage = ValidatePath(path);

            if (string.IsNullOrEmpty(returnObject.errorMessage))
            {
                string[] fileLines = System.IO.File.ReadAllLines(path);
                string[] fileLine;
                               
                foreach (string line in fileLines)
                {
                    fileLine = null;
                    fileLine = line.Split(";");

                    if (fileLine.Length >= 2)
                        reqInformations.Add(new RequestInformation() { category = fileLine[0], year = fileLine[1] });
                }

                if (reqInformations.Count <= 0)
                {
                    returnObject.errorMessage = $"There is no valid information in the file '{path}'.";
                }
                else
                {
                    requestReturn.requests = reqInformations.ToArray();
                    returnObject.file = requestReturn;
                }

            }
            
            return returnObject;                
        }

        public string ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "The path was not set.";

            if (!System.IO.File.Exists(path))
                return $"The file '{path}' not exists.";

            return String.Empty;
        }
    }
}
