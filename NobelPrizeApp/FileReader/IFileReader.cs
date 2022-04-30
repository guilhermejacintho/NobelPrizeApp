using System;
using System.Collections.Generic;
using System.Text;
using NobelPrizeApp.Model;

namespace NobelPrizeApp.FileReader
{
    public interface IFileReader
    {        
        FileInformation<NobelPrizeRequest> ReadFile(string path);
    }
}
