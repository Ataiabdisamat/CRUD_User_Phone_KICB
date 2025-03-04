﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneUserKICB.BLL.Validations
{
    /// <summary>
    /// Validator for phone
    /// </summary>
    public static class PhoneNumberValidation
    {
        public static bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;
            var regex = new Regex(@"^\+?\d{10,14}$"); //TODO
            return regex.IsMatch(phoneNumber);
        }
    }
}
