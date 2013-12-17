using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace STLTapReport.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [MyEmailAddress]//Custom Email Validation Attribute in STLTapReport/Models
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
   
    }
}