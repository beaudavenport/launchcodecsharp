using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STLTapReport.Models
{
    public class BeerModel
    {
        public string BeerName { get; set; }
        public string BeerStyle { get; set; }
        public string CountryOfOrigin { get; set; }
        public int ABV { get; set; }
    }
}