using System.Collections.Generic;

namespace StyleguideGenerator.Models.Data
{
    /// <summary>
    /// Раздел 
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Выделенное описание
        /// </summary>
        public string HighlightedDescription { get; set; }
        /// <summary>
        /// Дополнительное описание
        /// </summary>
        public string SubDescription { get; set; }
        /// <summary>
        /// Группы параметров 
        /// </summary>
        public List<SectionOptionsGroup> OptionsGroups { get; set; }
        /// <summary>
        /// Примеры
        /// </summary>
        public List<Sample> Samples { get; set; }  
        /// <summary>
        /// Подразделы
        /// </summary>
        public List<Section> ChildList { get; set; }
    }
}
