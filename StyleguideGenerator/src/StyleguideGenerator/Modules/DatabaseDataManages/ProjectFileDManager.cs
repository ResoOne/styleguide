using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Models.Database;
using StyleguideGenerator.Modules.Database;

namespace StyleguideGenerator.Modules.DatabaseDataManages
{
    public class ProjectFileDManager
    {
        public ProjectFile GetProjectFileById(int id)
        {
            ProjectFilesQuerys.SelectFileById.Parameters.SetParamsValues(id);
            var transaction = new DbTransaction(ProjectFilesQuerys.SelectFileById);
            var result = QueryExecuter.Execute(transaction);
            var p = result[0] as ProjectFile;
            return p;
        }
    }
}
