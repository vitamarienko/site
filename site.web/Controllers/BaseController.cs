using site.core.DataSvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;

namespace site.web.Controllers
{
    public class BaseController : Controller
    {
        protected static GoogleDriveOptions Options = new GoogleDriveOptions
        {
            ApplicationName = "vita marienko photography",
            SecretFileName = "client_secret.json"
        };

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var categories = MemoryCache.Default.Get("categories") as List<GoogleDriveFolder>;

            if (categories == null)
            {
                var svc = new PhotoDataSvc(new GoogleDriveSvcFactory(Options));
                categories = Task.Run(async () => await svc.GetCategoriesAsync()).Result;

                MemoryCache.Default.Set("categories", categories, new CacheItemPolicy());
            }

            ViewBag.Categories = categories;
        }
    }
}