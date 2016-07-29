using Microsoft.AspNetCore.Mvc;
using StyleguideGenerator.Modules.Database;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StyleguideGenerator.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectDbManager mg = new ProjectDbManager();
        // GET: /<controller>/
        public IActionResult Show(string name = null)
        {
            if (name == null)
            {
                var list = mg.GetProjectList();
                return View("AllProjects", list);
            }
            var project = mg.GetProjectByName(name);
            return View(project);
        }

        public IActionResult All()
        {
            var list = mg.GetProjectList();
            return View("AllProjects", list);
        }
    }
}
