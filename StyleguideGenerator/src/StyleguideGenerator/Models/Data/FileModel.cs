using System.Collections.Generic;
using StyleguideGenerator.Modules;

namespace StyleguideGenerator.Models.Data
{
    /// <summary>
    /// Файл в проекте
    /// </summary>
    public class ProjectFile
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string FilesystemName { get; set; }

        public string Source { get; set; }

        public string Author { get; set; }

        public ProjectFileType Type { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }
        public List<SelectorsLine> SelectorsLines { get; set; }

        public ProjectFile(string name, string source, string fsname)
        {
            Name = name;
            Source = source;
            FilesystemName = fsname;
            NonProjectFiles.Project.AddFile(this);
            Type=ProjectFileType.None;
        }
        public static bool ParseSourse(ProjectFile file)
        {
            file.SelectorsLines = CssParseModule.Parse(file.Source);
            return true;
        }
    }

    public enum ProjectFileType
    {
        None,
        Css,
        Js
    }
}
