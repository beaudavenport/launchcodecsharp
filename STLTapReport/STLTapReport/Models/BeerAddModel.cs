using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STLTapReport.data;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STLTapReport.Models
{
    public class BeerAddModel
    {
        public int beerID { get; set; }

        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public double abv { get; set; }
        public int styleID { get; set; }
        public string brewerylink { get; set; }
        public string imageurl { get; set; }

        public virtual style style { get; set; }
        public virtual ICollection<user> users { get; set; }

        //Adding SelectList to beer model
        public SelectList stylelist { get; set; }

    }
}