using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneUserKICB.BLL.Validations
{
    public static class PhoneNumberValidation
    {
        public static bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;
            var regex = new Regex(@"^\+?\d{10,15}$"); //TODO
            return regex.IsMatch(phoneNumber);
        }
    }
}
