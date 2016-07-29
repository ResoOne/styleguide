using System.Collections.Generic;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Models.Database;

namespace StyleguideGenerator.Modules.Database
{
    public class ProjectDbManager
    {
        public List<ProjectView> GetProjectList()
        {
            var list = new List<ProjectView>();
            var transaction = new DbTransaction(ProjectsDbQuerys.SelectProject);
            var result = QueryExecuter.Execute(transaction);
            list = result[0] as List<ProjectView>;
            return list;
        }
        public void NewProject(Project project)
        {
            ProjectsDbQuerys.NewProject.Parameters.SetParamsValues(project.Name, project.Author, project.Description, DatabaseSpecFormats.FormatToDatetime(project.Created));
            var transaction = new DbTransaction(ProjectsDbQuerys.NewProject);
            var result = QueryExecuter.Execute(transaction);
        }
        public void EditProject(Project project)
        {
            ProjectsDbQuerys.EditProject.Parameters.SetParamsValues(project.ID,project.Name,project.Description);
            var transaction = new DbTransaction(ProjectsDbQuerys.EditProject);
            var result = QueryExecuter.Execute(transaction);
        }
        public void DeleteProject(int id)
        {
            ProjectsDbQuerys.DeleteProject.Parameters.SetParamsValues(id);
            var transaction = new DbTransaction(ProjectsDbQuerys.DeleteProject);
            var result = QueryExecuter.Execute(transaction);
        }

        public Project GetProjectByName(string name)
        {
            ProjectsDbQuerys.SelectProjectByName.Parameters.SetParamsValues(name);
            ProjectFilesQuerys.SelectFilesByProjectName.Parameters.SetParamsValues(name);
            var transaction = new DbTransaction(ProjectsDbQuerys.SelectProjectByName, ProjectFilesQuerys.SelectFilesByProjectName);
            var result = QueryExecuter.Execute(transaction);
            var project = result[0] as Project;
            var files = result[1] as List<ProjectFile>;
            foreach (var f in files)
            {
                project.AddFile(f);
            }
            return project;
        }
    }
}
