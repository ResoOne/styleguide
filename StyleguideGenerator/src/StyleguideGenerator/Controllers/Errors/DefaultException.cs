using System;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Mvc;
using StyleguideGenerator.Models;

namespace StyleguideGenerator.Controllers.Errors
{
    /// <summary>
    /// Контроллер ошибки приложения
    /// </summary>
    public class DefaultException : Controller
    {        
        [Route("error")]
        public IActionResult Index()
        {
            var exception = new GlobalException();
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            exception.Dt = DateTime.Now;
            exception.UserLogin = HttpContext.User.Identity.Name ?? "defaultex no authenticated user";
            exception.Message = feature.Error.Message;
            exception.StackTrace = feature.Error.StackTrace;

            var isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return new ObjectResult(exception)
                {
                    StatusCode = 500,
                    DeclaredType = typeof(GlobalException)
                };
            }
            else
            {
                ViewData.Model = exception;
                return View("~/Views/Errors/DefaultException.cshtml");
            }            
        }
    }
}
