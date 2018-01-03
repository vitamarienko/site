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
            dataSvc.ResetCache();
            
            return RedirectToAction("Index");
        }
    }
}
