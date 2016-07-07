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
            var s = username + "_" + Guid.NewGuid().ToString("N") + "_" + filename;
            return s;
        }
    }
}
