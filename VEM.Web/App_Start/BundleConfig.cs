using System.Web;
using System.Web.Optimization;

namespace VEM.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Media/GlpbalCss")
                .Include("~/Media/css/bootstrap.min.css",
                        "~/Media/css/bootstrap-responsive.min.css",
                        "~/Media/css/font-awesome.min.css",
                        "~/Media/css/style-metro.css",
                        "~/Media/css/style.css",
                        "~/Media/css/style-responsive.css",
                        "~/Media/css/default.css",
                        "~/Media/css/uniform.default.css"));

      
            bundles.Add(new ScriptBundle("~/Media/CorePlugins")
                .Include("~/Media/js/jquery-1.10.2.min.js",
                        "~/Media/js/jquery-migrate-1.2.1.min.js",
                        "~/Media/js/jquery-ui-1.10.1.custom.min.js",
                        "~/Media/js/bootstrap.min.js",
                        "~/Media/js/jquery.slimscroll.min.js",
                        "~/Media/js/jquery.blockui.min.js",
                        "~/Media/js/jquery.cookie.min.js",
                        "~/Media/js/jquery.uniform.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/Media/App")
                .Include("~/Media/myjs/App.js"));
         

            bundles.Add(new ScriptBundle("~/Media/UtilPlugins")
                .Include(
                        "~/Media/myjs/CommonScript.js",
                        "~/Media/myjs/Status.js"
                     ));
         
        }
    }
}
