using System;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.FileReader
{
    interface IFileReader
    {
        string[] ReadFile(string path);
    }
}
