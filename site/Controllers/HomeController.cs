using site.core.DataSvc;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace site.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var options = new GoogleDriveOptions
            {
                ApplicationName = "vita marienko photography",
                SecretFileName = "client_secret.json"
            };

            var svc = new PhotoDataSvc(new GoogleDriveSvcFactory(options));
            var categories = await svc.GetCategoriesAsync();

            ViewBag.Categories = categories;
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}