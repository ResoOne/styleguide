using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;

namespace StyleguideGenerator.Controllers.Errors
{
    public class StatusCodeController : BaseController
    {
        public StatusCodeController(IHostingEnvironment hostEnvironment) : base(hostEnvironment)
        {
        }

        [Route("error/{code:int}")]
        // GET: /<controller>/
        public IActionResult Index(int code)
        {
            if (code == 404) { }
            ViewBag.code = code;
            return View("~/Views/Errors/StatusCode.cshtml");
        }
    }
}
