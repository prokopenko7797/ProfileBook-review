using ProfileBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Servcies.Settings
{
    public interface ISettingsManager
    {
        int SortBy { get; set; }
        string Lang { get; set; }
        int Theme { get; set; }
    }
}
