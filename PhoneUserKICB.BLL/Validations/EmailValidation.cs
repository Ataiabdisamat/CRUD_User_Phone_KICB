using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneUserKICB.BLL.Validations
{
    /// <summary>
    /// Validator for email
    /// </summary>
    public static class EmailValidation
    {
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
    }
}
