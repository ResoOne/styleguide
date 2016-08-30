using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StyleguideGenerator.Modules
{
    public static class FilesystemFileNameModule
    {
        public static string SysFileName(string username, string filename)
        {

            var s = Guid.NewGuid().ToString("N").Substring(0,8) + "-" + filename;
            return s;
        }
    }
}
