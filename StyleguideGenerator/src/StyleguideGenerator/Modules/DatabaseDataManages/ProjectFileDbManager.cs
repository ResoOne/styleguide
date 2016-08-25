using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Models.Database;

namespace StyleguideGenerator.Modules.Database
{
    public class ProjectFileDbManager
    {
        public ProjectFile GetProjectFileById(int id)
        {
            ProjectFileDbQuerys.SelectFileById.Parameters.SetParamsValues(id);
            var transaction = new DbTransaction(ProjectFileDbQuerys.SelectFileById);
            var result = QueryExecuter.Execute(transaction);
            var p = result[0] as ProjectFile;
            return p;
        }

        public void NewProjectFile(ProjectFile projectFile)
        {
            ProjectFileDbQuerys.NewProjectFile.Parameters.SetParamsValues(projectFile.Name, projectFile.FilesystemName,
                projectFile.ProjectID, projectFile.Type, projectFile.Author, projectFile.Created, projectFile.Source);
            var transaction = new DbTransaction(ProjectFileDbQuerys.NewProjectFile);
            var result = QueryExecuter.Execute(transaction);
        }
        public void EditProjectFile(ProjectFile projectFile)
        {
            ProjectFileDbQuerys.EditProjectFile.Parameters.SetParamsValues(projectFile.ID,projectFile.Name, projectFile.FilesystemName,
                projectFile.ProjectID, projectFile.Type, projectFile.Source);
            var transaction = new DbTransaction(ProjectFileDbQuerys.EditProjectFile);
            var result = QueryExecuter.Execute(transaction);
        }
        public void DeleteProjectFile(int id)
        {
            ProjectDbQuerys.DeleteProject.Parameters.SetParamsValues(id);
            var transaction = new DbTransaction(ProjectDbQuerys.DeleteProject);
            var result = QueryExecuter.Execute(transaction);
        }
    }
}
