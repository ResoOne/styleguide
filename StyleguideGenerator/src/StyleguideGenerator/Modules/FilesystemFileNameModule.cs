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

            var s = username + "/" + Guid.NewGuid().ToString("N") + "-" + filename;
            return s;
        }
    }
}
