using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using StyleguideGenerator.Models.Errors;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace StyleguideGenerator.Infrastructure
{

    public class GlobalExceptionFilter : IExceptionFilter/*, IDisposable*/
    {
        //private readonly ILogger _logger;

        //public GlobalExceptionFilter(ILoggerFactory logger)
        //{
        //    if (logger == null)
        //    {
        //        throw new ArgumentNullException(nameof(logger));
        //    }

        //    this._logger = logger.CreateLogger("Global Exception Filter");
        //}

        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse()
            {
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };
            var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                context.Result = new ObjectResult(response)
                {
                    StatusCode = 500,
                    DeclaredType = typeof(ErrorResponse)
                };
            }
            else
            {
                var modelMetadataProvider = context.HttpContext.ApplicationServices.GetRequiredService<IModelMetadataProvider>();
                var viewDataDictionary = new ViewDataDictionary<ErrorResponse>(modelMetadataProvider, new ModelStateDictionary());
                viewDataDictionary.Model = response;

                context.Result = new ViewResult()
                {
                    ViewName = "~/Views/Shared/TransferHandleErrorPage.cshtml",
                    ViewData = viewDataDictionary
                };                
            }
            //this._logger.LogError("GlobalExceptionFilter", context.Exception);
        }
    }
}


//var model = new UserError();

//model.ErrorType = "TRANSFER_H_ERROR";
//model.UserLogin = HttpContext.Current.User.Identity.Name;
//model.Url = HttpContext.Current.Request.RawUrl;
//model.AjaxRequest = new HttpRequestWrapper(HttpContext.Current.Request).IsAjaxRequest();
//model.Method = ex.TargetSite.Name;
//model.Sourse = ex.TargetSite.DeclaringType?.FullName;

//if (ex.Data.Contains("CustomTitle"))
//    model.Title = ex.Data["CustomTitle"].ToString();

//if (!string.IsNullOrEmpty(ex.Message))
//    model.ErrorsList.Add(ex.Message);

//if (model.AjaxRequest)
//{
//    if (ex.Data.Contains("CustomJsonResult") && (bool)ex.Data["CustomJsonResult"])
//        filterContext.Result = new JsonResult()
//        {
//            Data = model.JsonText,
//            JsonRequestBehavior = JsonRequestBehavior.AllowGet
//        };
//    else
//        filterContext.Result = new ContentResult()
//        {
//            Content = model.ViewText,
//            ContentType = "text"
//        };
//    HttpContext.Current.Response.TrySkipIisCustomErrors = true;
//    HttpContext.Current.Response.StatusCode = 403;
//}
//else
//{
//    filterContext.Result = new ViewResult()
//    {
//        ViewName = "~/Views/Shared/TransferHandleErrorPage.cshtml",
//        ViewData = new ViewDataDictionary(model)
//    };
//}
//_logger.Error(model.LogText);