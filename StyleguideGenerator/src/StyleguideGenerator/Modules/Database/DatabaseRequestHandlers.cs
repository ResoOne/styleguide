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
                ReadList.Clear();
                while (reader.Read())
                {
                    ReadList.Add(ProcessResponse(reader));
                }
            }
            return ReadList;
        }

        public abstract T ProcessResponse(SqliteDataReader reader);
    }
    public abstract class RequestHandlerOneRead<T> : IBaseDbRequestHandler where T : new()
    {
        public object Request(SqliteCommand command)
        {
            var row = new T();
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                row = ProcessResponse(reader);
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
