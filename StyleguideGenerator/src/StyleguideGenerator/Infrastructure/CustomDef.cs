using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StyleguideGenerator.Infrastructure
{
    public static class CustomDef
    {
        public static string UserFileLoadPath => Path.Combine(Directory.GetCurrentDirectory(), @"UserLoadFiles");
        public static string UserFileLoadRequestPath => "/LoadFiles";
    }
}
