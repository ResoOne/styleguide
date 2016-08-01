using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace StyleguideGenerator.Modules.Database
{
    public interface IBaseDbRequestHandler
    {
        object Request(SqliteCommand command);
    }
    public abstract class RequestHandlerRead<T> : IBaseDbRequestHandler
    {
        public object Request(SqliteCommand command)
        {
            var ReadList = new List<T>();
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReadList.Add(ProcessResponse(reader));
                    }
                }
            }
            return ReadList;
        }
        public abstract T ProcessResponse(SqliteDataReader reader);
    }
    public abstract class RequestHandlerOneRead<T> : IBaseDbRequestHandler where T : class
    {
        public object Request(SqliteCommand command)
        {
            T row = null;
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader.HasRows) row = ProcessResponse(reader);
            }
            return row;
        }

        public abstract T ProcessResponse(SqliteDataReader reader);
    }
    public class RequestHandlerScalar : IBaseDbRequestHandler
    {
        public object Request(SqliteCommand command)
        {
            return command.ExecuteScalar();
        }
    }
    public class RequestHandlerWithout : IBaseDbRequestHandler
    {
        public object Request(SqliteCommand command)
        {
            return command.ExecuteNonQuery();
        }
    }
}
