using System;

namespace StyleguideGenerator.Models.System
{
    /// <summary>
    /// Объект ошибки приложения 
    /// </summary>
    public class GlobalException : Exception
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        public GlobalException(string m, string t) : base(m)
        {
            Title = t;
        }

        public GlobalException(string m) : base(m)
        {
            Title = "Application Exception";
        }
    }
    /// <summary>
    /// Объект ошибки приложения для отображения
    /// </summary>
    public class GlobalExceptionView
    {
        /// <summary>
        /// Ошибка приложения
        /// </summary>
        public Exception AppException { get; set; }
        /// <summary>
        /// Время
        /// </summary>
        public DateTime Dt { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLogin { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary
        public string Title { get; set; }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message => AppException?.Message ?? "Exception";
        /// <summary>
        /// Stack Trace
        /// </summary>
        public string StackTrace => AppException?.StackTrace ?? "Exception Stack Trace";

        public GlobalExceptionView(Exception ex)
        {
            AppException = ex;
            if ((AppException as GlobalException) != null)
            {
                Title = (AppException as GlobalException).Title;
            }
            else
            {
                Title = "Application Exception";
            }
        }
    }

    /// <summary>
    /// Ошибка - Не задан параметр запроса
    /// </summary>
    public class EmptyParameterValueException : GlobalException
    {
        public EmptyParameterValueException(string m, string t) : base(m, t)
        {
        }
        public EmptyParameterValueException(string m) : base(m, "Empty Parameter Value Exception")
        {
        }
        public EmptyParameterValueException() : base("Не задан параметр", "Empty Parameter Value Exception")
        {
        }
    }
    /// <summary>
    /// Ошибка - Не правильный параметр запроса
    /// </summary>
    public class IncorrectParameterValueException : GlobalException
    {
        public IncorrectParameterValueException(string m,string t):base(m,t)
        {
        }
        public IncorrectParameterValueException(string m) : base(m, "Incorrect Parameter Value Exception")
        {
        }
        public IncorrectParameterValueException() : base("Не правильный параметр", "Incorrect Parameter Value Exception")
        {
        }
    }
    /// <summary>
    /// Ошибка - Нет объекта в базе данных
    /// </summary>
    public class EmptyObjectFromDatabase : GlobalException
    {
        public EmptyObjectFromDatabase(string m, string t):base(m,t)
        {
        }
        public EmptyObjectFromDatabase(string m) : base(m, "Empty object exception")
        {
        }
        public EmptyObjectFromDatabase() : base("Нет объекта в базе данных", "Empty object exception")
        {
        }
    }
}
