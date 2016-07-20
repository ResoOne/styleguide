using System.Collections.Generic;

namespace StyleguideGenerator.Models.Data
{
    /// <summary>
    /// Группа параметров раздела
    /// </summary>
    public class SectionOptionsGroup
    {
        /// <summary>
        /// Список параметров раздела
        /// </summary>
        private List<SectionOption> Vars { get; set; }
    }
    /// <summary>
    /// Параметр раздела
    /// </summary>
    public class SectionOption
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
    }
}
