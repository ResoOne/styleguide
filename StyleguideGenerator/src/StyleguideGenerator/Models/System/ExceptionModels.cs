using System;

namespace StyleguideGenerator.Models
{
    public abstract class BaseException
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Объект ошибки приложения
    /// </summary>
    public class GlobalException: BaseException
    {
        /// <summary>
        /// Время
        /// </summary>
        public DateTime Dt { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLogin { get; set; }
        
        /// <summary>
        /// Вызовы
        /// </summary>
        public string StackTrace { get; set; }
    }

    /// <summary>
    /// Ответ сервера с ошибкой
    /// </summary>
    public class ErrorResponse : BaseException
    {
        /// <summary>
        /// Вызовы
        /// </summary>
        public string StackTrace { get; set; }
    }

    public class CommonAppEx : BaseException
    {
    }
}
