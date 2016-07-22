using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

namespace StyleguideGenerator.Modules.Database
{
    public enum QueryType
    {
        NonQuery,
        Scalar,
        Reader
    }

    public abstract class BaseExecuteQuery
    {
        protected readonly static string DbFileName = "SggDb.db";

        public void Execute(string query, Dictionary<string, object> parameters)
        {
            var csb = new SqliteConnectionStringBuilder() { DataSource = Path.GetFullPath(DbFileName) };

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
                    Request(dbCommand);
                    transaction.Commit();
                }
            }
        }
        public abstract void Request(SqliteCommand command);
    }

    //public static class BdQueryExecuterFactory
    //{
    //    public static ExecuteQueryWithRead<T> GetExecuter(QueryType.Reader)
    //}


    public class ExecuteQueryWithRead<T>: BaseExecuteQuery where T : new()
    {
        public List<T> ReadList; 

        public override void Request(SqliteCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                ReadList.Clear();
                while (reader.Read())
                {
                    ReadList.Add(ProcessResponse(reader));
                }
            }
        }

        public T ProcessResponse(SqliteDataReader reader)
        {
            return new T();
        }
    }

    

    public class ExecuteQueryWithScalar<T>: BaseExecuteQuery
    {
        public override void Request(SqliteCommand command)
        {
            ProcessResponse(command.ExecuteScalar());
        }

        public T ProcessResponse(object responseObject)
        {
            return default(T);
        }
    }

    public class ExecuteQueryWithout : BaseExecuteQuery
    {
        public override void Request(SqliteCommand command)
        {
            ProcessResponse(command.ExecuteNonQuery());
        }

        public int ProcessResponse(int responseStatus)
        {
            return responseStatus;
        }
    }


    public static class DbManager
    {
        private static string DbFileName = "SggDb.db";

        private static SqliteConnectionStringBuilder _connectionStringBuilder = null;

        public static string GetConnectionString()
        {
            if (_connectionStringBuilder == null)
            {
                _connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = Path.GetFullPath(DbFileName) };
            }
            return _connectionStringBuilder.ToString();
        }
        
        public static void ExecuteQueryWithRead<T>(DbTransaction dbTransaction ,List<T> readList,Func<SqliteDataReader,T> readAction )
        {
            using (var connection = new SqliteConnection(DbManager.GetConnectionString()))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var query in dbTransaction.DbQueries)
                    {
                        var dbCommand = connection.CreateCommand();
                        dbCommand.Transaction = transaction;
                        dbCommand.CommandText = query.QueryText;

                        if (query.Parameters != null)
                        {
                            foreach (var el in query.Parameters)
                            {
                                dbCommand.Parameters.AddWithValue(el.Name, el.Value);
                            }
                        }
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                readList.Add(readAction(reader));
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
        }
    }


    public class DbTransaction
    {
        public List<DbQuery> DbQueries { get; set; }
    }

    public class DbQuery
    {
        public string QueryText { get; set; }

        public List<DbQueryParameter> Parameters { get; set; }

        public QueryType Type { get; set; }
    }

    public class DbQueryParameter
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }
}
