using Microsoft.Data.Sqlite;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Modules.Database;

namespace StyleguideGenerator.Models.Database
{
    public static class ProjectFileQuerys
    {
        public static DbQuery SelectFilesByProjectName = new DbQuery()
        {
            QueryText = "Select* FROM ProjectFiles WHERE ProjectId = (SELECT ID FROM Projects WHERE Name LIKE @name)",
            Parameters = new DbQueryParameters("@name"),
            Handler = typeof(ProjectFileListDbHandler)
        };

        public static DbQuery SelectFileById = new DbQuery()
        {
            QueryText = @"Select f.ID,f.Name,f.FsName,f.Type,f.Author,f.Created,f.Sourse,p.Name as pname,p.ID as pId FROM ProjectFiles as f
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

    public class ProjectFileDbHandler : RequestHandlerOneRead<ProjectFile>
    {
        public override ProjectFile ProcessResponse(SqliteDataReader reader)
        {
            var ID = int.Parse(reader["ID"].ToString());
            var name = reader["Name"].ToString();
            var author = reader["Author"].ToString();
            var sourse = reader["Sourse"].ToString();
            var fsname = reader["FsName"].ToString();
            var projname = reader["pname"].ToString();
            var projid = int.Parse(reader["pId"].ToString());
            var file = new ProjectFile(name, sourse, fsname);
            file.ID = ID;
            file.Author = author;
            file.Type = (ProjectFileType)int.Parse(reader["Type"].ToString());
            file.ProjectID = reader.IsDBNull(7) ? int.Parse(reader["ProjectID"].ToString()) : 0;
            var proj = new Project() {ID = projid, Name = projname};
            proj.AddFile(file);
            return file;
        }
    }
}
