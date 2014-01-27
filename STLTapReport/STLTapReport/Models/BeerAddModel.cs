using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STLTapReport.data;
using System.Web.Mvc;

namespace STLTapReport.Models
{
    public class BeerAddModel
    {
        public int beerID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double abv { get; set; }
        public int styleID { get; set; }
        public string beeradvocatelink { get; set; }

        public virtual style style { get; set; }
        public virtual ICollection<user> users { get; set; }

        //Adding SelectList to beer model
        public SelectList stylelist { get; set; }

    }
}