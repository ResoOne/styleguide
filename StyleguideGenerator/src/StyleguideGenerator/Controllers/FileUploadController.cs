using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using StyleguideGenerator.Modules;
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
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, bool parse = false)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
            foreach (var file in files)
                if (file != null && file.Length > 0)
                {
                    var disp = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    var fileName = disp.FileName.Trim('"');
                    var fileContent = "";
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    await file.SaveAsAsync(Path.Combine(uploads, fileName));

                    
                    if (parse)
                    {
                        
                    }
                }
            return View();
        }
    }
}
