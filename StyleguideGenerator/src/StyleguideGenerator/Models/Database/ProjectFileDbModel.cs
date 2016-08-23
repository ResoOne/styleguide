using System;
using Microsoft.Data.Sqlite;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Modules.Database;

namespace StyleguideGenerator.Models.Database
{
    public static class ProjectFileDbQuerys
    {
        public static DbQuery SelectFilesByProjectName = new DbQuery()
        {
            QueryText = "Select * FROM ProjectFiles WHERE ProjectId = (SELECT ID FROM Projects WHERE Name LIKE @name)",
            Parameters = new DbQueryParameters("@name"),
            Handler = typeof(ProjectFileListDbHandler)
        };

        public static DbQuery SelectFileById = new DbQuery()
        {
            QueryText = @"Select f.ID,f.Name,f.FsName,f.Type,f.Author,f.Created,f.Sourse,p.Name as ProjectName,p.ID as ProjectId FROM ProjectFiles as f
                            LEFT JOIN Projects as p
                            ON p.ID = f.ProjectId
                            WHERE f.Id = @id",
            Parameters = new DbQueryParameters("@id"),
            Handler = typeof (ProjectFileDbHandler)
        };
    }
    public class ProjectFileListDbHandler : RequestHandlerRead<ProjectFile>
    {
        public override ProjectFile ProcessResponse(SqliteDataReader reader)
        {
            var name = reader["Name"].ToString();
            var created = DatabaseSpecFormats.FormatStringToDt(reader["Created"].ToString());
            var file = new ProjectFile(name, created);
            file.ID = int.Parse(reader["ID"].ToString());
            file.Author = reader["Author"].ToString();
            file.Type = (ProjectFileType)int.Parse(reader["Type"].ToString());
            file.FilesystemName = reader["FsName"].ToString();
            file.Source = reader["Sourse"].ToString();
            file.ProjectID = int.Parse(reader["ProjectId"].ToString());
            return file;
        }
    }

    public class ProjectFileDbHandler : RequestHandlerOneRead<ProjectFile>
    {
        public override ProjectFile ProcessResponse(SqliteDataReader reader)
        {
            var name = reader["Name"].ToString();
            var created = DatabaseSpecFormats.FormatStringToDt(reader["Created"].ToString());
            var file = new ProjectFile(name, created);
            file.ID = int.Parse(reader["ID"].ToString());
            file.Author = reader["Author"].ToString();
            file.Type = (ProjectFileType)int.Parse(reader["Type"].ToString());
            file.FilesystemName = reader["FsName"].ToString();
            file.Source = reader["Sourse"].ToString();
            var projectID = reader.IsDBNull(7) ? int.Parse(reader["ProjectId"].ToString()) : (int)DbNoSelect.Project;
            var projectName = reader["ProjectName"].ToString();
            file.Project = new Project() { ID = projectID, Name = projectName };
            return file;
        }
    }
}
