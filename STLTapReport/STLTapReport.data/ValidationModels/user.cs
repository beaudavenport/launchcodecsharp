using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STLTapReport.data
{
    [MetadataType(typeof(userMetaData))]
    public partial class user
    {
    }

    public class userMetaData
    {
        [Required(ErrorMessage = "A user name is required.")]
        public string name { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        public string password { get; set; } 
    }
}
