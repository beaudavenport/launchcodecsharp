using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace STLTapReport.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "A user name is required.")]
        public string name { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Prove you're human, please.")]
        public string HumanVal { get; set; }
    }
}