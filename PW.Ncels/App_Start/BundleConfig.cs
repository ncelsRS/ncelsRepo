using System.Web;
using System.Web.Optimization;

namespace PW.Ncels {
    public class BundleConfig {
        
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                         "~/Content/css/plugins/iCheck/custom.css"));
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
            "~/Scripts/js/plugins/iCheck/icheck.min.js"));

            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                   "~/Content/css/plugins/dataTables/datatables.min.css"));
            // jeditable 
            bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                      "~/Scripts/js/plugins/jeditable/jquery.jeditable.js"));
            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/js/plugins/dataTables/datatables.min.js",
                      "~/Scripts/js/plugins/dataTables/angular-datatables.min.js",
                      "~/Scripts/js/plugins/dataTables/angular-datatables.buttons.min.js"
                      ));

            // Select2 Styless
            bundles.Add(new StyleBundle("~/plugins/select2Styles").Include(
                      "~/Content/css/plugins/ui-select/select.min.css"));

            // Select2
            bundles.Add(new ScriptBundle("~/plugins/select2").Include(
                      "~/Scripts/js/plugins/ui-select/select.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/css/plugins/datapicker/angular-datapicker.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/js/plugins/datapicker/angular-datepicker.js",
                      "~/Scripts/js/plugins/datapicker/datePickerPW.js"));

            bundles.Add(new ScriptBundle("~/plugins/moment").Include(
                      "~/Scripts/js/plugins/moment/moment.min.js"));

            bundles.Add(new StyleBundle("~/plugins/wizardStepsStyles").Include(
               "~/Content/css/plugins/steps/jquery.steps.css"));

            // wizardSteps 
            bundles.Add(new ScriptBundle("~/plugins/wizardSteps").Include(
                      "~/Scripts/js/plugins/staps/jquery.steps.min.js"));

            // validate 
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/js/plugins/validate/jquery.validate.min.js"));



            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/css/plugins/summernote/summernote.css",
                      "~/Content/css/plugins/summernote/summernote-bs3.css"
                      ));


            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Scripts/js/plugins/summernote/summernote.min.js",
                      "~/Scripts/js/plugins/summernote/angular-summernote.min.js"
                      ));

            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/jsTree").Include(
                      "~/Scripts/js/plugins/jsTree/jstree.min.js",
                      "~/Scripts/js/plugins/jsTree/ngJsTree.min.js"
                      ));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Content/plugins/jsTree").Include(
                      "~/Content/css/plugins/jsTree/style.css"));


            bundles.Add(new ScriptBundle("~/plugins/footable").Include(
                      "~/Scripts/js/plugins/footable/footable.all.min.js",
                      "~/Scripts/js/plugins/footable/angular-footable.js"
                      ));
            bundles.Add(new StyleBundle("~/Content/plugins/footable").Include(
              "~/Content/css/plugins/footable/footable.core.css"));


            bundles.Add(new ScriptBundle("~/plugins/sweetalert").Include(
                      "~/Scripts/js/plugins/sweetalert/sweetalert.min.js",
                      "~/Scripts/js/plugins/sweetalert/angular-sweetalert.min.js"
                      ));
            bundles.Add(new StyleBundle("~/Content/plugins/sweetalert").Include(
                          "~/Content/css/plugins/sweetalert/sweetalert.css"));
            // jsTree styles
            bundles.Add(new ScriptBundle("~/plugins/notify").Include(
                      "~/Scripts/js/plugins/angular-notify/angular-notify.min.js"
                      ));
            bundles.Add(new StyleBundle("~/Content/plugins/notify").Include(
                          "~/Content/css/plugins/angular-notify/angular-notify.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/clientCtrl").Include(
                    "~/Scripts/photon/client/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/expertCtrl").Include(
                        "~/Scripts/photon/expert/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/logonCtrl").Include(
                        "~/Scripts/photon/logon/*Ctrl.js"));

            bundles.Add(new ScriptBundle("~/bundles/baseCtrl").Include(
                        "~/Scripts/photon/base/*.js",
                        "~/Scripts/photon/base/obk/*.js"
                        ));

            // Angular UI Grid
            bundles.Add(new ScriptBundle("~/bundles/ui-grid").Include(
                "~/Scripts/js/plugins/ui-grid/ui-grid.min.js"));

            // Angular UI Grid Styles
            bundles.Add(new StyleBundle("~/Content/plugins/ui-grid").Include(
              "~/Content/css/plugins/ui-grid/ui-grid.min.css"));
        }
    }
}
