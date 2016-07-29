using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
