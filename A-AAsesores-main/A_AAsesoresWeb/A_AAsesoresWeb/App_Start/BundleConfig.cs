using System.Web.Optimization;

namespace A_AAsesoresWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                             "~/assets/js/jquery.min.js",
                             "~/assets/js/popper.min.js",
                             "~/assets/js/bootstrap.min.js",
                             "~/assets/js/modernizr.min.js",
                             "~/assets/js/waves.js",
                             "~/assets/js/jquery.slimscroll.js",
                             "~/assets/js/jquery.nicescroll.js",
                             "~/assets/js/jquery.scrollTo.min.js"));
            #endregion


            #region CSS

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/css/bootstrap.min.css",
                      "~/assets/css/icons.css",
                      "~/assets/css/style.css"));
            #endregion

        }
    }
}
