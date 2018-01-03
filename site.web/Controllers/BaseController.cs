using site.core.DataSvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using site.web.Utils;

namespace site.web.Controllers
{
    public class BaseController : Controller
    {
        protected PhotoDataSvcWrapper dataSvc;

        public BaseController()
        {
            dataSvc = new PhotoDataSvcWrapper();
        }

        protected List<GoogleDriveFolder> Categories;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            Categories = Task.Run(() => dataSvc.SeedCacheAsync()).Result;

            ViewBag.Categories = Categories;
        }
    }
}