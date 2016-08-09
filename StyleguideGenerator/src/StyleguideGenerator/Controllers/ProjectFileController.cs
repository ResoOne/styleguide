using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StyleguideGenerator.Models.Data;
using StyleguideGenerator.Models.System;
using StyleguideGenerator.Modules;
using StyleguideGenerator.Modules.DatabaseDataManages;

namespace StyleguideGenerator.Controllers
{
    public class ProjectFileController : BaseController
    {

        private static readonly string UploadFolder = "uploads";

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

        [HttpGet]
        public IActionResult New(int projectId = -1)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(IFormFile file = null, ClientProjectFile clfile = null, bool loadFile = true)
        {
            if (loadFile)
            {
                if (file != null && file.Length > 0)
                {
                    var uploads = Path.Combine(_hostEnvironment.WebRootPath, UploadFolder);
                    var disp = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    var fileName = disp.FileName.Trim('"').Replace(" ", "_");
                    string fileContent = null;
                    var sysFileName = FilesystemFileNameModule.SysFileName(UserName, fileName);
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        fileContent = reader.ReadToEnd();
                    }


                    using (var fileStream = new FileStream(Path.Combine(uploads, sysFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var saveFile = new ProjectFile(fileName, DateTime.Now);
                    saveFile.FilesystemName = sysFileName;
                    saveFile.Source = fileContent;
                    return View(saveFile);
                }
                else RequestErrors.Add("Не добавлен файл для загрузки");
            }
            else
            {
                if (clfile == null) RequestErrors.Add("Нет объекта для создания файла");
                else
                {
                    var saveFile = new ProjectFile(clfile.Name, DateTime.Now);
                    saveFile.Source = clfile.Source;
                    saveFile.Type = clfile.Type;
                    saveFile.ProjectID = clfile.ProjectId;
                    if (RequestErrors.Count > 0) return View(clfile);

                }
            }

            ViewBag.Errors = RequestErrors;
            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id = -1)
        {
            if (id == -1) throw new EmptyParameterValueException();
            ProjectFileDManager mg = new ProjectFileDManager();
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
