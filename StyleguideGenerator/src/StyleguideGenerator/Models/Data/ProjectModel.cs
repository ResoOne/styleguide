using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StyleguideGenerator.Models.Data
{
    /// <summary>
    /// Проект
    /// </summary>
    public class Project
    {
        /// <summary>
        /// ID проекта
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Название проекта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Файлы проекта
        /// </summary>
        public List<ProjectFile> FileList { get; set; }

        /// <summary>
        /// Файлы проекта
        /// </summary>
        public int FileCount {
            get {
                return FileList!=null ? FileList.Count() : _fileCount;
            }
            set { _fileCount = value; } }
        
        private int _fileCount { get; set; }

        public void AddFile(ProjectFile file)
        {
            if (FileList.Contains(file)) return;
            file.Project?.FileList.Remove(file);
            FileList.Add(file);
            file.Project = this;
        }

        public void DelFile(ProjectFile file)
        {
            FileList.Remove(file);
            NonProjectFiles.Project.FileList.Add(file);
            file.Project = NonProjectFiles.Project;
        }
    }

    public static class NonProjectFiles
    {
        public static readonly Project Project;

        static NonProjectFiles()
        {
            Project = new Project
            {
                Name = "No project files",
                Author = "System",
                Description = "Файлы без проекта",
                FileList = new List<ProjectFile>()
            };
        }
    }
}
