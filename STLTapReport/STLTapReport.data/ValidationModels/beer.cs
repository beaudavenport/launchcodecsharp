using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STLTapReport.data
{
    [MetadataType(typeof(beerMetaData))]
    public partial class beer
    {

    }
    public class beerMetaData
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public double abv { get; set; }

    }
}
