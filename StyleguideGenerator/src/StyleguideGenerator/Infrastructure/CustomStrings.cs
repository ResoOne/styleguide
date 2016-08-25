using System.IO;

namespace StyleguideGenerator.Infrastructure
{
    /// <summary>
    /// Заданные общие строки
    /// </summary>
    public static class CustomStrings
    {
        public static string UserFileLoadPath = Path.Combine(Directory.GetCurrentDirectory(), @"UserLoadFiles");
        public static string UserFileLoadRequestPath = "/LoadFiles";
    }
}
