using System.Web;
using System.Web.Optimization;

namespace My_e_commerceProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
               bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                      "~/assests/js/vendor/modernizr-3.5.0.min.js",
                      "~/assests/js/vendor/jquery-1.12.4.min.js",
                      "~/assets/js/popper.min.js",
                      "~/assets/js/bootstrap.min.js",
                      "~/assets/js/owl.carousel.min.js",
                      "~/assests/js/slick.min.js",
                      "~/assests/js/jquery.slicknav.min.js",
                      "~/assests/js/wow.min.js",
                      "~/assests/js/jquery.magnific-popup.js",
                      "~/assests/js/jquery.nice-select.min.js",
                      "~/assests/js/jquery.counterup.min.js",
                      "~/assests/js/waypoints.min.js",
                      "~/assests/js/price_rangs.js",
                      "~/assests/js/contact.js",
                      "~/assests/js/jquery.form.js",
                      "~/assests/js/jquery.validate.min.js",
                      "~/assests/js/mail-script.js",
                      "~/assests/js/jquery.ajaxchimp.min.js",
                      "~/assests/js/plugins.js",
                      "~/assests/js/main.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                 "~/assests/css/bootstrap.min.css",
                 "~/assests/css/owl.carousel.min.css",
                 "~/assests/css/slicknav.css",
                 "~/assests/css/flaticon.css",
                 "~/assests/css/animate.min.css",
                 "~/assests/css/price_rangs.css",
                 "~/assests/css/magnific-popup.css",
                 "~/assests/css/fontawesome-all.min.css",
                 "~/assests/css/themify-icons.css",
                 "~/assests/css/slick.css",
                 "~/assests/css/nice-select.css",
                 "~/assests/css/style.css"));

            

        }
    }
}
