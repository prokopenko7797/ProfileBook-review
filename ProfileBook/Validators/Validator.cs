﻿using System.Text.RegularExpressions;

namespace ProfileBook.Validators
{
    public static class Validator
    {
        #region ______Public Methods______
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
            var inRange = new Regex($@"^(.{{{min}}}|.{{{max}}})$");

            return inRange.IsMatch(str);
        }

        public static bool StartWithNumeral(string str)
        {
            var hasNumber = new Regex(@"^[0-9]");

            return hasNumber.IsMatch(str);
        }

        #endregion

    }
}
