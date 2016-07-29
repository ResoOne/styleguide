using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using StyleguideGenerator.Models.Database;

namespace StyleguideGenerator.Modules.Database
{
    public static class QueryExecuter
    {
        private readonly static string DbFileName = "SggDb.db";

        private static SqliteConnectionStringBuilder _connectionStringBuilder = null;

        public static string GetConnectionString()
        {
            if (_connectionStringBuilder == null)
            {
                _connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = Path.GetFullPath(DbFileName) };
            }
            return _connectionStringBuilder.ToString();
        }
        public static List<object> Execute(DbTransaction dbTransaction)
        {
            var csb = new SqliteConnectionStringBuilder() { DataSource = Path.GetFullPath(DbFileName) };
            List<object> result = new List<object>();
            using (var connection = new SqliteConnection(GetConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var query in dbTransaction.Queries)
                    {
                        var handler = (IBaseDbRequestHandler)Activator.CreateInstance(query.Handler);
                        var dbCommand = connection.CreateCommand();
                        dbCommand.Transaction = transaction;
                        dbCommand.CommandText = query.QueryText;
                        if (query.Parameters != null)
                        {
                            foreach (var el in query.Parameters)
                            {
                                dbCommand.Parameters.AddWithValue(el.Key, el.Value);
                            }
                        }
                        result.Add(handler.Request(dbCommand));
                    }
                    transaction.Commit();
                }
            }
            return result;
        }
    }
}
