using System.Web;
using System.Web.Optimization;

namespace MeetingManagementSystem
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            #region Common JS

            //bundles.Add(new ScriptBundle("~/bundles/CommonJS").Include(
            //             "~/Content/Klorofil/assets/vendor/jquery/jquery.min.js",
            //             "~/Content/Klorofil/assets/vendor/bootstrap/js/bootstrap.min.js"
            //            ));

            #endregion

            #region Common CSS

            //bundles.Add(new StyleBundle("~/Content/CommonCSS").Include(
            //            //"~/Content/bootstrap.css",
            //            "~/Content\bootstrap.min.css"

            //            ));
            #endregion

            #region fonts

            //bundles.Add(new StyleBundle("~/bundles/fontAwesome").Include(
            //            "~/Content/font-awesome/css/font-awesome.min.css"));

            #endregion

            #region Datatable Bundle

            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                "~/Scripts/lib/DataTables_1_10_13/media/js/jquery.dataTables.min.js",
                "~/Scripts/lib/DataTables_1_10_13/media/js/dataTables.bootstrap.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/FixedColumns/js/dataTables.fixedColumns.min.js",
                "~/Scripts/lib/DataTables_1_10_13/media/js/dataTables.rowsGroup.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/dataTables.buttons.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/buttons.bootstrap.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Export/jszip.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Export/pdfmake.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Export/vfs_fonts.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/buttons.html5.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/buttons.print.min.js"
                ));

            bundles.Add(new StyleBundle("~/styles/DataTable").Include(
                "~/Scripts/lib/DataTables_1_10_13/media/css/dataTables.bootstrap.min.css",
                "~/Scripts/lib/DataTables_1_10_13/extensions/FixedColumns/css/fixedColumns.bootstrap.min.css",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/css/buttons.dataTables.min.css"
                ));

            #endregion

            #region Klorofil
            bundles.Add(new StyleBundle("~/bundles/klorofilcss").Include(

                "~/Content/Klorofil/assets/vendor/bootstrap/css/bootstrap.min.css",
                "~/ Content/Klorofil/assets/vendor/font-awesome/css/font-awesome.min.css",
                "~/Content/Klorofil/assets/vendor/linearicons/style.css",
                "~/Content/Klorofil/assets/vendor/chartist/css/chartist-custom.css",
                "~/Content/Klorofil/assets/css/main.css",
                "~/Content/Klorofil/assets/css/demo.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/klorofiljs").Include(

                "~/Content/Klorofil/assets/vendor/jquery/jquery.min.js",
                "~/Content/Klorofil/assets/vendor/bootstrap/js/bootstrap.min.js",
                "~/Content/Klorofil/assets/vendor/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/Klorofil/assets/vendor/jquery.easy-pie-chart/jquery.easypiechart.min.js",
                "~/Content/Klorofil/assets/vendor/chartist/js/chartist.min.js",
                "~/Content/Klorofil/assets/scripts/klorofil-common.js"

               ));

            #endregion
        }
    }
}
