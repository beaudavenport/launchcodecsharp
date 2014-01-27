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
        [Required]
        public string password { get; set; }
        
        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }

    }
}
