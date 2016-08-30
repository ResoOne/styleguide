using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StyleguideGenerator.Models.Data
{
    /// <summary>
    /// Базовая модель проекта
    /// </summary>
    public class BaseProject
    {
        /// <summary>
        /// ID проекта
        /// </summary>
        [Required]
        public int ID { get; set; }
        /// <summary>
        /// Название проекта
        /// </summary>
        [RegularExpression(@"^\S+$", ErrorMessage = "Имя должно быть без пробелов"), Required]
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
        /// Readme
        /// </summary>
        public string Readme { get; set; }
    }
    /// <summary>
    /// Проект
    /// </summary>
    public class Project : BaseProject
    {
        /// <summary>
        /// Файлы проекта
        /// </summary>
        public List<ProjectFile> FileList { get; set; }

        /// <summary>
        /// Файлы проекта
        /// </summary>
        public int FileCount
        {
            get { return FileList != null ? FileList.Count() : 0; }
        }

        public Project()
        {
            Description = "";
            Name = "project";
            FileList = new List<ProjectFile>();
        }

        public void AddFile(ProjectFile file)
        {
            FileList.Add(file);
            file.Project = this;
        }

        public void DelFile(ProjectFile file)
        {
            FileList.Remove(file);
            file.Project = NonProjectFiles.Project;
        }
    }
    /// <summary>
    /// Модель проекта для списка
    /// </summary>
    public class ProjectView : BaseProject
    {
        /// <summary>
        /// Файлы проекта
        /// </summary>
        public int FileCount { get; set; }

    }

    /// <summary>
    /// Для файлов без проектов
    /// </summary>
    public static class NonProjectFiles
    {
        public static readonly Project Project;

        public static ProjectView ProjectView
        {
            get
            {
                return new ProjectView
                {
                    Name = Project.Name,
                    Author = Project.Author,
                    Description = Project.Description,
                    FileCount = Project.FileCount,
                    ID = Project.ID,
                    Readme = ""
                };
            }
        }
        static NonProjectFiles()
        {
            Project = new Project
            {
                Name = "No project files",
                Author = "System",
                Description = "Файлы без проекта",
                FileList = new List<ProjectFile>(),
                ID=0,
                Readme = ""
            };
        }
    }
}
