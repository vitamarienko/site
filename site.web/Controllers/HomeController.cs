using site.web.Utils;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace site.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            dataSvc = new PhotoDataSvcWrapper();
        }

        private PhotoDataSvcWrapper dataSvc;

        public async Task<ActionResult> Index()
        {
            var categoryId = ConfigurationManager.AppSettings["1XERe9TfEb3pWTm5Efo6o0hxvIcXZmcWY"] 
                ?? Categories.FirstOrDefault()?.Id 
                ?? "";

            return RedirectToAction("Category", new { id = categoryId });
        }

        public async Task<ActionResult> Category(string id)
        {
            var titles = await dataSvc.GetByCategoryAsync(id);
            ViewBag.Title = Categories.FirstOrDefault(e => e.Id == id)?.Name;

            return View(titles);
        }

        [HttpGet]
        [Route("Category/{categoryId}/View/{id}", Name = "View")]
        public new async Task<ActionResult> View(string categoryId, string id, string title)
        {
            var items = await dataSvc.GetSessionAsync(categoryId, id);

            ViewBag.Title = title;

            return View(items);
        }

        [Authorize]
        public ActionResult Reset()
        {
            var cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }

            return RedirectToAction("Index");
        }
    }
}
