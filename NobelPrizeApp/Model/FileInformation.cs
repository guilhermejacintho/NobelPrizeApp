using System;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.Model
{
    public class FileInformation<I>
    {
        public I file;
        public Boolean isEmpty { get => file == null; }
        public string errorMessage;
    }
}
