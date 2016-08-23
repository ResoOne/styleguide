using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Modules.Database;

namespace StyleguideGenerator.Controllers
{
    public class ProjectsController : BaseController
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
            var s = CommonMark.CommonMarkSettings.Default.OutputFormat = CommonMark.OutputFormat.Html;
            ViewBag.ParsedMark = CommonMark.CommonMarkConverter.Convert(project.Readme);

            return View(project);
        }

        public IActionResult All()
        {
            var list = mg.GetProjectList();
            return View("AllProjects", list);
        }


        public ActionResult NewPr(string name = "non")
        {
            ProjectDbManager mg = new ProjectDbManager();
            mg.NewProject(new Project() { Name = name, Author = UserName, Description = "test project from view", Created = DateTime.Now });
            var list = mg.GetProjectList();
            return View("Pr", list);
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
            if (!ModelState.IsValid) return View(project);
            try
            {
                mg.EditProject(project);

            }
            catch
            {
                return View(project);
            }
            return new RedirectResult(Url.Action("Show", new { name = project.Name }));
        }

        

        public ActionResult DelPr(int id = -1, string name = "update")
        {
            ProjectDbManager mg = new ProjectDbManager();
            if (id != -1)
            {
                mg.DeleteProject(id);
            }
            var list = mg.GetProjectList();
            return View("Pr", list);
        }

        public ActionResult EditPr(int id = -1, string name = "update")
        {
            ProjectDbManager mg = new ProjectDbManager();
            if (id != -1)
            {
                mg.EditProject(new Project() { Name = name, ID = id });
            }
            var list = mg.GetProjectList();
            return View("Pr", list);
        }
        [HttpPost]
        public IActionResult RemoveFile(int id = -1)
        {
            return View();
        }
    }
}
// multi file upload
/*
public async Task<IActionResult> AddFile(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, UploadFolder);
            foreach (var file in files)
*/
