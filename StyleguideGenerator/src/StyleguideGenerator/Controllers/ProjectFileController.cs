using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StyleguideGenerator.Models.System;
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
            if (id == -1) throw new EmptyParameterValueException();
            ProjectFileDManager mg = new ProjectFileDManager();
            var file = mg.GetProjectFileById(id);
            if (file == null) throw new EmptyObjectFromDatabase();
            return View(file);
        }
    }
}
