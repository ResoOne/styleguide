using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StyleguideGenerator.Infrastructure;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Models.System;
using StyleguideGenerator.Modules;
using StyleguideGenerator.Modules.Database;
using SFile = System.IO.File;

namespace StyleguideGenerator.Controllers
{
    public class ProjectFileController : BaseController
    {
        private static readonly string UploadFolder = "uploads";

        private ProjectFileDbManager mg = new ProjectFileDbManager();

        public IActionResult Show(int id = -1)
        {
            if (id == -1) throw new EmptyParameterValueException();
            var file = mg.GetProjectFileById(id);
            var d = Directory.GetCurrentDirectory();
            if (file == null) throw new EmptyObjectFromDatabase();
            return View(file);
        }

        [HttpGet]
        public IActionResult New(int project = -1)
        {
            ProjectDbManager mg = new ProjectDbManager();
            ViewBag.ProjectsList = mg.GetProjectList(true);
            TempData["ProjectsList"]= ViewBag.ProjectsList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(IFormFile file = null, ClientProjectFile clfile = null, bool loadFile = true)
        {
            if (TempData.ContainsKey("ProjectsList")) ViewBag.ProjectsList = TempData["ProjectsList"];
            TempData["ProjectsList"] = ViewBag.ProjectsList;
            var saveFile = new ProjectFile();
            if (loadFile)
            {
                if (file != null && file.Length > 0)
                {
                    var disp = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    var fileName = disp.FileName.Trim('"').Replace(" ", "_");
                    string fileContent = null;
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    saveFile.Name = fileName;
                    saveFile.Type = FileTypeCheckModule.Check(fileName);
                    saveFile.Source = fileContent;
                }
                else RequestErrors.Add("Не добавлен файл для загрузки");
            }
            else
            {
                if (clfile == null) RequestErrors.Add("Нет объекта для создания файла");
                else
                {
                    saveFile.Name = clfile.Name;
                    saveFile.Source = clfile.Source;
                    saveFile.Type = clfile.Type;
                    saveFile.ProjectID = clfile.ProjectId;
                }
            }
            if (RequestErrors.Count > 0) return View(clfile);
            saveFile.Author = UserName;
            if (!loadFile) saveFile.Name += ".txt";
            saveFile.FilesystemName = FilesystemFileNameModule.SysFileName(UserName, saveFile.Name);
            //if (loadFile)
            //{
            //    using (var fileStream = new FileStream(CustomStrings.UserFileLoadPath, FileMode.Create))
            //    {
            //        await file.CopyToAsync(fileStream);
            //    }
            //}
            //else
            //{
            var userPath = Path.Combine(CustomStrings.UserFileLoadPath, UserName);
            Directory.CreateDirectory(userPath);
            using (var fileStream = SFile.CreateText(Path.Combine(userPath, saveFile.FilesystemName)))
            {
                await fileStream.WriteAsync(saveFile.Source);
            }
            //}

            mg.NewProjectFile(saveFile);

            ViewBag.Errors = RequestErrors;
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id = -1)
        {
            if (id == -1) throw new EmptyParameterValueException();
            var file = mg.GetProjectFileById(id);
            if (file == null) throw new EmptyObjectFromDatabase();
            return View(file);
        }

        [HttpPost]
        public IActionResult Edit(ProjectFile file)
        {
            return View();
        }

        public IActionResult Delete(int id = -1)
        {
            return View();
        }

        [NonAction]
        public bool CheckModel(ProjectFile file)
        {
            if (string.IsNullOrEmpty(file.Name)) RequestErrors.Add("Не указано имя файла");
            if (string.IsNullOrEmpty(file.Source)) RequestErrors.Add("Не указан текст файла");
            return RequestErrors.Count < 1;

        }
    }
}
