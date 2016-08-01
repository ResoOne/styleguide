using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StyleguideGenerator.Models;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Modules.Database;
using StyleguideGenerator.Modules.DatabaseDataManages;

namespace StyleguideGenerator.Controllers
{
    public class ProjectFileController : BaseController
    {
        public ProjectFileController(IHostingEnvironment hostEnvironment) : base(hostEnvironment)
        {
        }

        public IActionResult Show(int id = -1)
        {
            if (id == -1) return View("Common/CommonError.cshtml", new CommonAppEx() { Message = "Нет файла" });
            ProjectFileDManager mg = new ProjectFileDManager();
            var file = mg.GetProjectFileById(id);
            if(file==null) return View("Common/CommonError.cshtml", new CommonAppEx() { Message = "Нет файла" });
            return View(file);
        }
    }
}
