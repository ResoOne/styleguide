using System;
using System.Collections.Generic;
using System.Linq;
using StyleguideGenerator.Modules.Database;

namespace StyleguideGenerator.Models.Database
{
    public class DbTransaction
    {
        public List<DbQuery> Queries { get; set; }

        public DbTransaction()
        {
            Queries = new List<DbQuery>();
        }

        public DbTransaction(params DbQuery[] queries)
        {
            Queries = new List<DbQuery>();
            Queries.AddRange(queries);
        }
    }

    public class DbQuery
    {
        public string QueryText { get; set; }

        public DbQueryParameters Parameters { get; set; }

        public Type Handler { get; set; }

        public DbQuery()
        {
            Parameters = null;
        }
    }

    public class DbQueryParameters : Dictionary<string, object>
    {
        public DbQueryParameters(params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                Add(names[i], null);
            }
        }

        public void SetParamsValues(params object[] values)
        {
            var l = values.Length;
            for (var i = 0; i < Keys.Count; i++)
            {
                var key = Keys.ElementAt(i);
                this[key] = null;
                if (i < l)
                {
                    var val = values[i];
                    if (val.GetType() == typeof(DateTime)) val = DatabaseSpecFormats.FormatDtToString((DateTime)val);
                    this[key] = val;
                }
                else break;
            }
        }

    }
}
