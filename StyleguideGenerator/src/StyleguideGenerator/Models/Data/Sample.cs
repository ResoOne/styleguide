using System.Collections.Generic;

namespace StyleguideGenerator.Models.Data
{
    /// <summary>
    /// Примеры раздела
    /// </summary>
    public class SectionSamples
    {
        /// <summary>
        /// HTML код для примера
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Отображение HTML кода
        /// </summary>
        public bool ShowCode { get; set; }
        /// <summary>
        /// Список примеров
        /// </summary>
        public List<Sample> Samples { get; set; }
    }
    /// <summary>
    /// Пример в разделе
    /// </summary>
    public class Sample
    {
        /// <summary>
        /// Модификатор для замены 
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Текс для отображения
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        public int Type { get; set; }
    }
}
