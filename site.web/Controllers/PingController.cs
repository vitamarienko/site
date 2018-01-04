using NLog;
using site.web.Utils;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace site.web.Controllers
{
    public class PingController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("ping");

            var dataSvc = new PhotoDataSvcWrapper();
            var dataIsGood = await dataSvc.TryGetAnyAsync();

            if (!dataIsGood)
            {
                logger.Info("updating data in cache");

                dataSvc.ResetCache();
                await dataSvc.SeedCacheAsync();

                return Content("seed data");
            }

            return Content("pong");
        }
    }
}