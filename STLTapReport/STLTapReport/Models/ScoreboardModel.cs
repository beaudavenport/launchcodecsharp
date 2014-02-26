using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STLTapReport.Models
{
    public class ScoreboardModel
    {
        public string FirstBeer { get; set; }
        public int FirstBCount { get; set; }
        public string SecondBeer { get; set; }
        public int SecondBCount { get; set; }
        public string ThirdBeer { get; set; }
        public int ThirdBCount { get; set; }

        public string FirstStyle { get; set; }
        public int FirstSCount { get; set; }
        public string SecondStyle { get; set; }
        public int SecondSCount { get; set; }
        public string ThirdStyle { get; set; }
        public int ThirdSCount { get; set; }
    }
}