using System.Collections.Generic;

namespace StyleguideGenerator.Models.Data
{
    /// <summary>
    /// Список разделов
    /// </summary>
    public class SectionTree
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Элементы списка
        /// </summary>
        public List<SectionTreeEl> Els { get; set; }
    }
    /// <summary>
    /// Элемент списка разделов
    /// </summary>
    public class SectionTreeEl
    {
        /// <summary>
        /// Индекс
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Индексная строка
        /// </summary>
        public string IndexString { get; set; }
        /// <summary>
        /// Подразделы
        /// </summary>
        public List<SectionTreeEl> SubEls { get; set; } 
    }
}
