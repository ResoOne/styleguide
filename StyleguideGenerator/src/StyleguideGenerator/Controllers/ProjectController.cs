﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Modules;
using StyleguideGenerator.Modules.Database;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StyleguideGenerator.Controllers
{
    public class ProjectsController : BaseController
    {
        private ProjectDManager mg = new ProjectDManager();

        public ProjectsController(IHostingEnvironment hostEnvironment) : base(hostEnvironment)
        {
        }

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

        [HttpGet]
        public IActionResult Edit(string name = null)
        {
            var project = mg.GetProjectByName(name);
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(Project project)
        {
            throw new System.Exception("1111");
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
