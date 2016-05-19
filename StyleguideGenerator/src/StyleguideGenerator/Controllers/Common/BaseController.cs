﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Extensions.PlatformAbstractions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StyleguideGenerator.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IApplicationEnvironment _appEnvironment;
        protected readonly IHostingEnvironment _hostEnvironment;
        protected string UserName;     

        public BaseController(IHostingEnvironment hostEnvironment)
        {
            _appEnvironment = PlatformServices.Default.Application;
            _hostEnvironment = hostEnvironment;
            UserName = User?.Identity?.Name ?? "no_autorize_user";            
        }
        
    }
}