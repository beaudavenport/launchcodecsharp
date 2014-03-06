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
        [Required(ErrorMessage="A beer name is required.")]
        public string name { get; set; }

        [Required(ErrorMessage="A description is required.")]
        public string description { get; set; }

        [Required(ErrorMessage="An ABV value is required in FLOAT format (e.g., 4.5).")]
        public double abv { get; set; }

    }
}
