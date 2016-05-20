using System;

namespace StyleguideGenerator.Models
{
    /// <summary>
    /// Объект ошибки приложения
    /// </summary>
    public class GlobalException
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
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Вызовы
        /// </summary>
        public string StackTrace { get; set; }
    }

    /// <summary>
    /// Ответ сервера с ошибкой
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Вызовы
        /// </summary>
        public string StackTrace { get; set; }
    }
}
