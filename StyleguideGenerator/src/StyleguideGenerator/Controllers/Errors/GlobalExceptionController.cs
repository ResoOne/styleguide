using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StyleguideGenerator.Models.System;

namespace StyleguideGenerator.Controllers.Errors
{
    /// <summary>
    /// Контроллер ошибки приложения
    /// </summary>
    public class GlobalExceptionController : BaseController
    {
        [Route("error")]
        public IActionResult Index()
        {
            bool transfer = false;
            var serverExeption = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            if (TransferExceptions.Contains(serverExeption.GetType())) transfer = true;
            var viewModel = new GlobalExceptionView(serverExeption);
            if (!transfer)
            {
                viewModel.Dt = DateTime.Now;
                viewModel.UserLogin = HttpContext.User.Identity.Name ?? "No_authenticated_user";
            }
            var isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return new ObjectResult(viewModel)
                {
                    StatusCode = 500,
                    DeclaredType = typeof(GlobalExceptionView)
                };
            }
            else
            {
                ViewData.Model = viewModel;
                if (transfer)
                {
                    return View("~/Views/Errors/TransferException.cshtml");
                }
                else
                {
                    return View("~/Views/Errors/DefaultException.cshtml");
                }
            }            
        }

        private static readonly List<Type> TransferExceptions = new List<Type>()
        {
            typeof(IncorrectParameterValueException),
            typeof(EmptyParameterValueException)
        };

        
    }

    
}
