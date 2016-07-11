using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Microsoft.AspNet.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using StyleguideGenerator.Models.System;

namespace StyleguideGenerator.Modules.Database
{
    public static class DbManager
    {
        private static string DbFileName = "SggDb.db";

        public static void ExecuteQuery(string query,bool read )
        {
            var csb = new SqliteConnectionStringBuilder() {DataSource = DbFileName};
            using (var connection = new SqliteConnection("" + csb))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.Transaction = transaction;
                    insertCommand.CommandText = "INSERT INTO Selectors ( Name ) VALUES ( $text )";
                    insertCommand.Parameters.AddWithValue("$text", "");
                    insertCommand.ExecuteNonQuery();

                    var selectCommand = connection.CreateCommand();
                    selectCommand.Transaction = transaction;
                    selectCommand.CommandText = "SELECT text FROM message";
                    if (read)
                    {
                        using (var reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var message = reader.GetString(0);
                                Console.WriteLine(message);
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        public static void ExecuteQueryWithRead<T>(string query,Dictionary<string,object> parameters,List<T> readList,Func<SqliteDataReader,T> readAction )
        {
            var csb = new SqliteConnectionStringBuilder() {DataSource = Path.GetFullPath(DbFileName)};
            
            using (var connection = new SqliteConnection("" + csb))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var dbCommand = connection.CreateCommand();
                    dbCommand.Transaction = transaction;
                    dbCommand.CommandText = query;
                    if (parameters != null)
                    {
                        foreach (var el in parameters)
                        {
                            dbCommand.Parameters.AddWithValue(el.Key, el.Value);
                        }
                    }
                    using (var reader = dbCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                readList.Add(readAction(reader));
                            }
                        }
                    transaction.Commit();
                }
            }
        }

    }
}
