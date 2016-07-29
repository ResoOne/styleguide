using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Modules.Database;

namespace StyleguideGenerator.Models.Database
{
    public static class ProjectFilesQuerys
    {
        public static DbQuery SelectFilesByProjectName = new DbQuery()
        {
            QueryText = "Select* FROM ProjectFiles WHERE ProjectId = (SELECT ID FROM Projects WHERE Name LIKE @name)",
            Parameters = new DbQueryParameters("@name"),
            Handler = typeof(ProjectFilesListDbHandler)
        };
    }

    public class ProjectFilesListDbHandler : RequestHandlerRead<ProjectFile>
    {
        public override ProjectFile ProcessResponse(SqliteDataReader reader)
        {
            var ID = int.Parse(reader["ID"].ToString());
            var name = reader["Name"].ToString();
            var author = reader["Author"].ToString();
            var sourse = reader["Sourse"].ToString();
            var fsname = reader["FsName"].ToString();
            var file = new ProjectFile(name, sourse, fsname);
            file.ID = ID;
            file.Author = author;
            file.Type = (ProjectFileType)int.Parse(reader["Type"].ToString());
            file.ProjectID = reader.IsDBNull(7)? int.Parse(reader["ProjectID"].ToString()) : 0;
            return file;
        }
    }
}
