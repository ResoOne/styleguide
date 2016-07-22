using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using StyleguideGenerator.Models.Data;

namespace StyleguideGenerator.Modules.Database
{
    public class ProjectDbManager
    {
        //private static DbQuery 
        private string SelectProjectQuery = "Select * FROM ProjectsList";
        private string NewProjectQuery = "INSERT INTO Projects (Name, Author, Description,Created)] VALUES (@name, @author, @desc, @crdt);";
        private string EditProjectQuery = "UPDATE Projects SET Name=@name, Author=@author, Description=@desc WHERE ID=@id;";
        private string DeleteProjectQuery = "DELETE FROM Projects WHERE ID=@id";

        public ProjectDbManager()
        {
            
        }


        public List<Project> GetProjectList()
        {
            var list = new List<Project>();
            //DbManager.ExecuteQueryWithRead(SelectProjectQuery,null,list,SelectReaderAction);
            return list;
        }

        public Project SelectReaderAction(SqliteDataReader rd)
        {
            var project= new Project();
            project.ID = int.Parse(rd["ID"].ToString());
            project.Name = rd["Name"].ToString();
            project.Author = rd["Author"].ToString();
            project.Created = DateTime.Parse(rd["Created"].ToString());
            project.Description = rd["Description"].ToString();
            project.FileCount = int.Parse(rd["FileCount"].ToString());
            return project;
        }

        public void NewProject(Project project)
        {
            
        }

        public void EditProject(int id)
        {
            
        }

        public void DeleteProject(int id)
        {
            
        }
    }
}
