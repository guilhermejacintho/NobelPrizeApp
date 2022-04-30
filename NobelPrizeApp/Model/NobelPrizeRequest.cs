using System;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.Model
{
    public class NobelPrizeRequest
    {
        public RequestInformation[] requests;
    }

    public class RequestInformation
    {
        public string category;
        public string year;
    }
}
