using System.Web;
using System.Web.Optimization;

namespace NaturalDateTime.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/NaturalDateTime/css").Include(
                "~/Content/css/master.css",
                "~/Content/css/examples.css",
                "~/Content/css/main.css"));

            bundles.Add(new ScriptBundle("~/bundles/NaturalDateTime/js").Include(
                "~/Scripts/app/homeController.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
