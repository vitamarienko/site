using NLog;
using site.web.Utils;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;

namespace site.web.Controllers
{
    public class PingController : Controller
    {
        public ActionResult Index()
        {
            HostingEnvironment.QueueBackgroundWorkItem(async ct => {

                var logger = LogManager.GetCurrentClassLogger();

                logger.Info("begin update cache");

                var dataSvc = new PhotoDataSvcWrapper();
                
                await dataSvc.SeedCacheAsync(true);

                logger.Info("end update cache");
            });

            return Content("pong");
        }
    }
}