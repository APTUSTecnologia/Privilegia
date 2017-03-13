using System.Web;
using System.Web.Optimization;

namespace Privilegia
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Calendar/moment.min.js",
                        "~/Scripts/sweetalert2.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                         "~/Scripts/Calendar/fullcalendar.js",
                        "~/Scripts/Calendar/locale/es.js",
                        "~/Scripts/jquery.dataTables.yadcf.js",
                        "~/Scripts/DataTables/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                         "~/Scripts/jquery.validate*",
                         "~/Scripts/jquery.unobtrusive*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/Calendar/fullcalendar.css",
                      "~/Content/privilegia.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/bootstrap-toggle.less"));

            

        }
    }
}
