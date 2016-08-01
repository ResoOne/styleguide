using System;
using System.Globalization;

namespace StyleguideGenerator.Modules.Database
{
    public static class DatabaseSpecFormats
    {
        public static string FormatToDatetime(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture);
        }
    }
}
