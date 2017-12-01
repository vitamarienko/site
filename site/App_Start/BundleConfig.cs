using System.Web;
using System.Web.Optimization;

namespace site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/adminscript")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/adminsite.js"));

            bundles.Add(new ScriptBundle("~/bundles/script")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/bundles/adminstyle")
                .Include("~/Content/normalize.css")
                .Include("~/Content/skeleton.css")
                .Include("~/Content/base.css")
                .Include("~/Content/adminstyle.css"));

            bundles.Add(new StyleBundle("~/bundles/style")
                .Include("~/Content/normalize.css")
                .Include("~/Content/skeleton.css")
                .Include("~/Content/base.css")
                .Include("~/Content/style.css"));
        }
    }
}
