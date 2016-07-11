using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace StyleguideGenerator.Modules.Converters
{
    public class BaseConverter
    {
        public virtual void ConvertFromBase(SqliteDataReader reader)
        {
            
        }
    }
}
