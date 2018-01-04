using site.web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace site.web.Controllers
{
    [Authorize]
    public class CacheController : Controller
    {
        PhotoDataSvcWrapper dataSvc;

        public CacheController()
        {
            dataSvc = new PhotoDataSvcWrapper();
        }

        public ActionResult Reset()
        {
            dataSvc.ResetCache();

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public ActionResult Drop()
        {
            dataSvc.ResetCache();

            return Content("drop cache");
        }
    }
}