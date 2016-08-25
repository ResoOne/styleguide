using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StyleguideGenerator.Modules;

namespace StyleguideGenerator.Models.Data
{

    public class BaseProjectFile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public ProjectFileType Type { get; set; }
        public int ProjectID { get; set; }
        public DateTime Created { get; set; }
    }
    /// <summary>
    /// Файл в проекте
    /// </summary>
    public class ProjectFile : BaseProjectFile
    {
        public string FilesystemName { get; set; }

        public string Source { get; set; }
        public Project Project { get; set; }
        public List<SelectorsLine> SelectorsLines { get; set; }

        public ProjectFile(string name,DateTime created)
        {
            Name = name;
            Source = null;
            FilesystemName = null;
            Project = NonProjectFiles.Project;
            Type=ProjectFileType.None;
            Created = created;
        }
        public ProjectFile()
        {
            Name = "";
            Source = null;
            FilesystemName = null;
            Project = NonProjectFiles.Project;
            Type = ProjectFileType.None;
            Created = DateTime.Now;
        }
    }

    public class ClientProjectFile
    {
        [Display(Name = "Sourse code")]
        [Required]
        public string Source { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Filetype")]
        public ProjectFileType Type { get; set; }
        [Display(Name = "Project ID")]
        public int ProjectId { get; set; }
    }

    public enum ProjectFileType
    {
        None,
        Css,
        Js
    }
}
