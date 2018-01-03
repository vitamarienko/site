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

        public async Task<ActionResult> Index()
        {
            var categoryId = ConfigurationManager.AppSettings["initialcategoryid"] 
                ?? Categories.FirstOrDefault()?.Id 
                ?? "";

            return RedirectToAction("Category", new { id = categoryId });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Category(string id)
        {
            var categoryId = Categories.First(e => e.Alias == id).Id;
            var titles = await dataSvc.GetByCategoryAsync(categoryId);
            ViewBag.Title = Categories.FirstOrDefault(e => e.Alias == id)?.Name;

            return View(titles);
        }

        [HttpGet]
        [Route("{categoryId}/{id}", Name = "View")]
        public new async Task<ActionResult> View(string categoryId, string id)
        {
            var category = Categories.First(e => e.Alias == categoryId);
            var session = category.Children.First(e => e.Alias == id);

            var items = await dataSvc.GetSessionAsync(category.Id, session.Id);

            ViewBag.Title = session.Name;

            return View(items);
        }

        [Authorize]
        public ActionResult Reset()
        {
            dataSvc.ResetCache();
            
            return RedirectToAction("Index");
        }
    }
}
