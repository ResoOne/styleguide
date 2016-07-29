﻿using System;
using Microsoft.Data.Sqlite;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Modules.Database;

namespace StyleguideGenerator.Models.Database
{
    public static class ProjectsDbQuerys
    {
        public static DbQuery SelectProject = new DbQuery()
        {
            QueryText = "Select * FROM ProjectsList",
            Handler = typeof(ProjectsListDbHandler)
        };
        public static DbQuery NewProject = new DbQuery()
        {
            QueryText = "INSERT INTO Projects (Name, Author, Description,Created) VALUES (@name, @author, @desc, @crdt);",
            Parameters = new DbQueryParameters("@name", "@author", "@desc", "@crdt"),
            Handler = typeof(RequestHandlerWithout)
        };
        public static DbQuery EditProject = new DbQuery()
        {
            QueryText = "UPDATE Projects SET Name=@name, Description=@desc WHERE ID=@id;",
            Parameters = new DbQueryParameters("@id", "@name", "@desc"),
            Handler = typeof(RequestHandlerWithout)
        };
        public static DbQuery DeleteProject = new DbQuery()
        {
            QueryText = "DELETE FROM Projects WHERE ID=@id",
            Parameters = new DbQueryParameters("@id"),
            Handler = typeof(RequestHandlerWithout)
        };

        public static DbQuery SelectProjectByName = new DbQuery()
        {
            QueryText = "Select * FROM Projects WHERE Name LIKE @name",
            Parameters = new DbQueryParameters("@name"),
            Handler = typeof (ProjectOneDbHandler)
        };

        public static DbQuery SelectProjectById = new DbQuery()
        {
            QueryText = "Select * FROM Projects WHERE ID=@id",
            Parameters = new DbQueryParameters("@id"),
            Handler = typeof (ProjectOneDbHandler)
        };

    }
    public class ProjectsListDbHandler : RequestHandlerRead<ProjectView>
    {
        public override ProjectView ProcessResponse(SqliteDataReader reader)
        {
            var project = new ProjectView();
            project.ID = int.Parse(reader["ID"].ToString());
            project.Name = reader["Name"].ToString();
            project.Author = reader["Author"].ToString();
            project.Created = DateTime.Parse(reader["Created"].ToString());
            project.Description = reader["Description"].ToString();
            project.FileCount = int.Parse(reader["FileCount"].ToString());
            return project;
        }
    }

    public class ProjectOneDbHandler : RequestHandlerOneRead<Project>
    {
        public override Project ProcessResponse(SqliteDataReader reader)
        {
            var project = new Project();
            project.ID = int.Parse(reader["ID"].ToString());
            project.Name = reader["Name"].ToString();
            project.Author = reader["Author"].ToString();
            project.Created = DateTime.Parse(reader["Created"].ToString());
            project.Description = reader["Description"].ToString();
            project.Readme = reader["Readme"].ToString();
            return project;
        }
    }
}
