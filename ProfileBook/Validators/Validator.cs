﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfileBook.Validators
{
    public static class Validator
    {
        public static bool Match(string str, string con)
        {
            return str.Equals(con);
        }

        public static bool HasUpLowNum(string str)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");

            return hasNumber.IsMatch(str) && hasUpperCase.IsMatch(str) && hasLowerCase.IsMatch(str);
        }

        public static bool InRange(string str, int min, int max)
        {
            return str.Length >= min  && str.Length <= max;
        }

        public static bool StartWithNumeral(string str)
        {
            var hasNumber = new Regex(@"^[0-9]");

            return hasNumber.IsMatch(str);
        }


    }
}
