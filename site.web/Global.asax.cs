using FluentScheduler;
using NLog;
using site.web.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace site.web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //JobManager.Initialize(new DataRegistry());
        }
    }

    //public class DataRegistry : Registry
    //{
    //    public DataRegistry()
    //    {
    //        Schedule(async
    //            () =>
    //            {
    //                var dataSvc = new PhotoDataSvcWrapper();
    //                var dataIsGood = await dataSvc.TryGetAnyAsync();

    //                if (!dataIsGood)
    //                {
    //                    var logger = LogManager.GetCurrentClassLogger();

    //                    logger.Info("updating data in cache");

    //                    dataSvc.ResetCache();
    //                    await dataSvc.SeedCacheAsync();
    //                }
    //            })
    //            .ToRunEvery(5)
    //            .Minutes();
    //    }
    //}
}
