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
    }
}
