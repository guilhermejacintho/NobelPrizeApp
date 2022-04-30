using System;
using System.Collections.Generic;
using System.Text;

namespace NobelPrizeApp.Model
{
    public class NobelPrizeInformation
    {
        public Prize[] prizes;
    }

    public class Prize
    {
        public string year;
        public string category;
        public Laureate[] laureates;
    }

    public class Laureate
    {
        public string id;
        public string firstname;
        public string surname;
        public string motivation;
        public string share;
    }

}
