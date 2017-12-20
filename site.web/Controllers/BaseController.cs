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
        protected List<GoogleDriveFolder> Categories;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            Categories = MemoryCache.Default.Get("categories") as List<GoogleDriveFolder>;

            if (Categories == null)
            {
                var dataSvc = new PhotoDataSvcWrapper();
                Categories = Task.Run(async () => await dataSvc.GetCategoriesAsync()).Result;

#if !DEBUG
                foreach (var category in categories)
                {
                    var byCategory = Task.Run(async () => await dataSvc.GetByCategoryAsync(category.Id)).Result;
                }
                
#endif
                MemoryCache.Default.Set("categories", Categories, new CacheItemPolicy());
            }

            ViewBag.Categories = Categories;
        }
    }
}