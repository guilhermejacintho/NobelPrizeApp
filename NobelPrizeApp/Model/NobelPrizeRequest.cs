using System;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.Model
{
    public class NobelPrizeRequest
    {
        public RequestInfromation[] requests;
    }

    public class RequestInfromation
    {
        public string category;
        public string year;
    }
}
