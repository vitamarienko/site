using site.core.DataSvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace site.web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Category(string id)
        {
            var categoryKey = $"category:{id}";
            var titles = MemoryCache.Default.Get(categoryKey) as List<GoogleDriveFolder>;

            if (titles == null)
            {
                var svc = new PhotoDataSvc(new GoogleDriveSvcFactory(Options));
                titles = await svc.GetByCategoryAsync(id);

                MemoryCache.Default.Set(categoryKey, titles, new CacheItemPolicy());
            }

            return View(titles);
        }

        public async Task<ActionResult> Reset()
        {
            var cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }

            return View("Index");
        }
    }
}
