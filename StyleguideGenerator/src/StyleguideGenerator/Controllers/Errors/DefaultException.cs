using System;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Mvc;
using StyleguideGenerator.Models.Errors;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StyleguideGenerator.Controllers.Errors
{
    public class DefaultException : Controller
    {
        // GET: /<controller>/
        [Route("error")]
        public IActionResult Index()
        {
            var response = new ErrorResponse();
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewData.Model = 
            response.Message = "Exception Message from controller";
            response.StackTrace = "stack trace stack trace";
            ViewData.Model = response;
            return View("~/Views/Errors/DefaultException.cshtml");
        }
    }
}
