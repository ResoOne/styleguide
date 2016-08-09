using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StyleguideGenerator.Models.Data;
using SFile = System.IO.File;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StyleguideGenerator.Controllers
{
    public class FileUploadController : BaseController
    {
        public FileUploadController(IHostingEnvironment hostEnvironment) : base(hostEnvironment)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, bool parse = true)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
            foreach (var file in files)
                if (file != null && file.Length > 0)
                {
                    var disp = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    
                    var fileName = disp.FileName.Trim('"').Replace(" ","_");
                    var fileContent = "";
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    //await file.SaveAsAsync(Path.Combine(uploads, g));
                    //if (parse)
                    //{
                    //    var unpfile = new ProjectFile(fileName);

                    //    ProjectFile.ParseSourse(unpfile);
                    //    ViewBag.st = fileContent;
                    //    return View("~/Views/Main/Index.cshtml");
                    //}
                }
            return View();
        }
    }
}
