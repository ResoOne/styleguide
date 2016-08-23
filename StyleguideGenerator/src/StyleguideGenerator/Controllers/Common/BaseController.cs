using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StyleguideGenerator.Controllers
{
    public class BaseController : Controller
    {
        //protected readonly IHostingEnvironment _hostEnvironment;
        protected string UserName;
        protected List<string> RequestErrors;

        public BaseController()
        {
            //_hostEnvironment = hostEnvironment;
            UserName = User?.Identity?.Name ?? "No_authenticated_user";
            RequestErrors = new List<string>();
        }
    }
}
