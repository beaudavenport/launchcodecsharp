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
        [StringLength(20), Required(ErrorMessage = "A user name is required and must be 20 characters or less.")]
        public string name { get; set; }

        [StringLength(15), Required(ErrorMessage = "A password is required and must be 15 characters or less.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Prove you're human, please.")]
        public string HumanVal { get; set; }
    }
}