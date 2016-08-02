using System.Collections.Generic;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Models.Database;

namespace StyleguideGenerator.Modules.Database
{
    public class ProjectDManager
    {
        public List<ProjectView> GetProjectList()
        {
            var list = new List<ProjectView>();
            var transaction = new DbTransaction(ProjectDbQuerys.SelectProject);
            var result = QueryExecuter.Execute(transaction);
            list = result[0] as List<ProjectView>;
            return list;
        }
        public void NewProject(Project project)
        {
            ProjectDbQuerys.NewProject.Parameters.SetParamsValues(project.Name, project.Author, project.Description, DatabaseSpecFormats.FormatToDatetime(project.Created));
            var transaction = new DbTransaction(ProjectDbQuerys.NewProject);
            var result = QueryExecuter.Execute(transaction);
        }
        public void EditProject(Project project)
        {
            ProjectDbQuerys.EditProject.Parameters.SetParamsValues(project.ID,project.Name,project.Description,project.Readme);
            var transaction = new DbTransaction(ProjectDbQuerys.EditProject);
            var result = QueryExecuter.Execute(transaction);
        }
        public void DeleteProject(int id)
        {
            ProjectDbQuerys.DeleteProject.Parameters.SetParamsValues(id);
            var transaction = new DbTransaction(ProjectDbQuerys.DeleteProject);
            var result = QueryExecuter.Execute(transaction);
        }

        public Project GetProjectByName(string name)
        {
            ProjectDbQuerys.SelectProjectByName.Parameters.SetParamsValues(name);
            ProjectFileQuerys.SelectFilesByProjectName.Parameters.SetParamsValues(name);
            var transaction = new DbTransaction(ProjectDbQuerys.SelectProjectByName, ProjectFileQuerys.SelectFilesByProjectName);
            var result = QueryExecuter.Execute(transaction);
            var project = result[0] as Project;
            var files = result[1] as List<ProjectFile>;
            if (project != null)
            {
                foreach (var f in files)
                {
                    project.AddFile(f);
                }
            }
            return project;
        }
    }
}
