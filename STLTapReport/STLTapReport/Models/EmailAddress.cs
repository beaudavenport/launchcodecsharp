using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace STLTapReport.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MyEmailAddressAttribute : ValidationAttribute //Inherits ValidationAttribute
    {
        //Customizes 'IsValid' method to check for email validation
        public override bool IsValid(object value)
        {
            string email = null;
            //Try-catch that converts the object to a string and checks for email fomat using 'MailAddress' from System.Net.Mail
            try
            {
                if (value != null)
                {
                    email = value.ToString();
                    MailAddress mail = new MailAddress(email);
                    return true;
                } else {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }

        }
    }
}
