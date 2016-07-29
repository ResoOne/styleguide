using Microsoft.AspNetCore.Mvc;
using StyleguideGenerator.Models.Data;
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

        [HttpGet]
        public IActionResult Edit(string name = null)
        {
            var project = mg.GetProjectByName(name);
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(Project project)
        {
            try
            {
                mg.EditProject(project);
            }
            catch
            {
                return View(project);
            }
            
            return new RedirectResult(Url.Action("Show",new {name=project.Name}));
        }
    }
}
