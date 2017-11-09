using System.Web;
using System.Web.Optimization;

namespace PW.Prism
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/kendo/2014.2.716/jquery.min.js",
                        "~/Scripts/jquery.signalR-0.5.2.min.js"
                        ));
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/2014.2.716/kendo.all.min.js",
                        "~/Scripts/kendo/2014.2.716/kendo.aspnetmvc.min.js",
                        "~/Scripts/kendo.modernizr.custom.js",
                        "~/Scripts/kendo/2014.2.716/cultures/kendo.culture.kk-KZ.min.js",
                        "~/Scripts/kendo/2014.2.716/cultures/kendo.culture.ru-RU.min.js",
                        "~/Scripts/kendo/2014.2.716/cultures/kendo.culture.en-US.min.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/prism").Include(
                        "~/Scripts/prism*"));

            bundles.Add(new ScriptBundle("~/bundles/ncels").Include(
                "~/Scripts/ncels*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/mobileDatePickerjs").Include(
                        "~/Scripts/mobile/picker.js",
                          "~/Scripts/mobile/picker.date.js",
                            "~/Scripts/mobile/legacy.js"));

            bundles.Add(new ScriptBundle("~/bundles/declaration").Include(
//                        "~/Scripts/js/custom/subform-comment.js",
                          "~/Scripts/js/custom/message-dialog-box.js",
                            "~/Scripts/js/custom/common.js",
                            "~/Scripts/js/custom/spin.js"
                            ));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
//                      "~/Scripts/js/plugins/chosen/chosen.js",
                      "~/Scripts/js/plugins/chosen/chosen.jquery.js",
                      "~/Scripts/js/plugins/jquery-ui/jquery-ui.js",
                      "~/Scripts/js/plugins/jquery-ui/ui.datepicker-ru.js",
                      "~/Scripts/js/plugins/select2/select2.js",
                      "~/Scripts/js/plugins/select2/select2_locale_ru.js",
                      "~/Scripts/js/plugins/jsTree/jstree.min.js",
                      "~/Scripts/js/plugins/dataTables/jquery.dataTables.min.js",
                      "~/Scripts/js/plugins/dataTables/jquery.dataTables.rowReordering.js"
                      )
                    );
            bundles.Add(new ScriptBundle("~/bundles/crypto").Include(
                "~/Scripts/crypto/eds.js", "~/Scripts/crypto/crypto_object_ext.js"));
            /*  bundles.Add(new ScriptBundle("~/bundles/js")
              .IncludeDirectory("~/Scripts/js/", "*.js", true));*/




            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendocss").Include(
              "~/Content/kendo/2014.2.716/kendo.common.min.css",
              "~/Content/kendo/2014.2.716/kendo.mobile.all.min.css",

              "~/Content/kendo/2014.2.716/kendo.dataviz.min.css",
              "~/Content/kendo/2014.2.716/kendo.default.min.css",
              "~/Content/kendo/2014.2.716/kendo.dataviz.default.min.css"
              ));

            bundles.Add(new StyleBundle("~/Content/mobileDatePickerCss").Include(
          "~/Content/mobile/default.css",
          "~/Content/mobile/default.date.css"
          ));



        }
    }
}
