using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StyleguideGenerator.Models.System;

namespace StyleguideGenerator.Controllers.Errors
{
    /// <summary>
    /// Контроллер ошибки приложения
    /// </summary>
    public class GlobalExceptionController : Controller
    {        
        [Route("error")]
        public IActionResult Index()
        {
            bool transfer = false;
            var serverExeption = HttpContext.Features.Get<IExceptionHandlerFeature>().Error as GlobalException;
            var exceptionType = serverExeption.GetType();
            if (TransferExceptions.Contains(serverExeption.GetType())) transfer = true;
            if (!transfer)
            {
                serverExeption.Dt = DateTime.Now;
                serverExeption.UserLogin = HttpContext.User.Identity.Name ?? "defaultex no authenticated user";
            }
            var isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return new ObjectResult(serverExeption)
                {
                    StatusCode = 500,
                    DeclaredType = serverExeption.GetType()
                };
            }
            else
            {
                ViewData.Model = serverExeption;
                if(transfer) return View("~/Views/Errors/TransferException.cshtml");
                else return View("~/Views/Errors/DefaultException.cshtml");
            }            
        }

        private static readonly List<Type> TransferExceptions = new List<Type>()
        {
            typeof(IncorrectParameterValueException),
            typeof(EmptyParameterValueException)
        };
    }

    
}
