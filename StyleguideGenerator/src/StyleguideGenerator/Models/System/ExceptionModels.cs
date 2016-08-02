using System;

namespace StyleguideGenerator.Models.System
{
    /// <summary>
    /// Объект ошибки приложения
    /// </summary>
    public class GlobalException: Exception
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Время
        /// </summary>
        public DateTime Dt { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLogin { get; set; }

        public GlobalException(string m,string t) : base(m)
        {
            Title = t;
        }
        public GlobalException(string m) : base(m)
        {
            Title = "Application Exception";
        }
    }


    public class EmptyParameterValueException : GlobalException
    {
        public EmptyParameterValueException(string m, string t) : base(m, t)
        {

        }
        public EmptyParameterValueException(string m) : base(m, "Empty Parameter Value Exception")
        {

        }

        public EmptyParameterValueException() : base("Не задан параметр", "Incorrect Parameter Value Exception")
        {

        }
    }
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
