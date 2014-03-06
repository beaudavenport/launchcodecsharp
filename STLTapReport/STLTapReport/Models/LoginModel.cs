using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STLTapReport.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*You forgot to enter your username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*You forgot to enter your password.")]
        public string Password { get; set; }
    }
}