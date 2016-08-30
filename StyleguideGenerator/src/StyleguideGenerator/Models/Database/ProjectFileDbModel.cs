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
        /// <summary>
        /// "@id"
        /// </summary>
        public static DbQuery SelectFileById = new DbQuery()
        {
            QueryText = @"Select f.ID,f.Name,f.FsName,f.Type,f.Author,f.Created,f.Source,p.Name as ProjectName,p.ID as ProjectId FROM ProjectFiles as f
                            LEFT JOIN Projects as p
                            ON p.ID = f.ProjectId
                            WHERE f.Id = @id",
            Parameters = new DbQueryParameters("@id"),
            Handler = typeof (ProjectFileDbHandler)
        };
        /// <summary>
        /// "@name", "@fsname", "@projectid", "@type", "@author", "@crdt", "@src"
        /// </summary>
        public static DbQuery NewProjectFile = new DbQuery()
        {
            QueryText = "INSERT INTO ProjectFiles (Name, FsName, ProjectId,Type,Author,Created,Source) VALUES (@name, @fsname, @projectid, @type,@author,@crdt,@src);",
            Parameters = new DbQueryParameters("@name", "@fsname", "@projectid", "@type", "@author", "@crdt", "@src"),
            Handler = typeof(RequestHandlerWithout)
        };
        /// <summary>
        /// "@id", "@name", "@fsname", "@projectid", "@type", "@src"
        /// </summary>
        public static DbQuery EditProjectFile = new DbQuery()
        {
            QueryText = "UPDATE ProjectFiles SET Name=@name,FsName=@fsname,ProjectId=@projectid,Type=@type,Source=@src WHERE ID=@id;",
            Parameters = new DbQueryParameters("@id", "@name", "@fsname", "@projectid", "@type", "@src"),
            Handler = typeof(RequestHandlerWithout)
        };
        public static DbQuery DeleteProjectFile = new DbQuery()
        {
            QueryText = "DELETE FROM ProjectFiles WHERE ID=@id",
            Parameters = new DbQueryParameters("@id"),
            Handler = typeof(RequestHandlerWithout)
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
            file.Source = reader["Source"].ToString();
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
            file.Source = reader["Source"].ToString();
            var projectID = reader.IsDBNull(7) ? int.Parse(reader["ProjectId"].ToString()) : (int)DbNoSelect.Project;
            var projectName = reader.IsDBNull(8) ? reader["ProjectName"].ToString() : NonProjectFiles.Project.Name;
            file.Project = new Project() { ID = projectID, Name = projectName };
            return file;
        }
    }
}
