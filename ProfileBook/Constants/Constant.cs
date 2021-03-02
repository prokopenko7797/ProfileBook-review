using ProfileBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProfileBook.Constants
{
    public static class Constant
    {
        public const string DB_Name = "sqlite.db";
        public const int MinLoginLength = 4;
        public const int MaxLoginLength = 16;
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 16;
        public const int NonAuthorized = -1;
        public const int SQLError = -1;
        public const string DefaultProfileImage = "pic_profile.png";
        public const int DefaultSort = (int)SortEnum.nick_name;
        public const string DefaultLanguage = "en";
        public const int DefaultTheme = (int)OSAppTheme.Unspecified;
    }
}
