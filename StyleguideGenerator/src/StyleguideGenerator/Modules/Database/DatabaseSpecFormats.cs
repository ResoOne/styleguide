using System;
using System.Globalization;

namespace StyleguideGenerator.Modules.Database
{
    public static class DatabaseSpecFormats
    {
        public static string FormatDtToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture);
        }

        public static DateTime FormatStringToDt(string datestring)
        {
            return DateTime.Parse(datestring);
        }
    }

    public enum DbNoSelect
    {
        Project = 0,
        ProjectFile = 0
    }
}
