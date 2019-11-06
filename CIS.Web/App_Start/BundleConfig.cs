using System.Web;
using System.Web.Optimization;

namespace CIS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Assets/admin/vendors/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                "~/Assets/client/CIS-pro/owlcarousel/owl.carousel.min.js",
                "~/Assets/admin/libs/toastr/toastr.min.js",
                "~/Assets/client/js/common.js"                
            ));

            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Assets/client/CIS-pro/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/CIS-pro/owlcarousel/css/owl.carousel.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/CIS-pro/owlcarousel/css/owl.theme.default.min.css", new CssRewriteUrlTransform())                
                .Include("~/Assets/client/CIS-pro/css/styleClone.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/CIS-pro/css/cisstyle.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/CIS-pro/css/fonts/icomoon/icomoon.css", new CssRewriteUrlTransform())
                .Include("~/Assets/admin/libs/toastr/toastr.min.css", new CssRewriteUrlTransform())
            );

            BundleTable.EnableOptimizations = true;
        }
    }
}
