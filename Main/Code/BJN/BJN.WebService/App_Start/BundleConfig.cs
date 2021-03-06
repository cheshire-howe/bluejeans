﻿using System.Web;
using System.Web.Optimization;

namespace BJN.WebService
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.datetimepicker.full.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/create").Include(
                        "~/Scripts/bluejeans/create/recurring.js"));

            bundles.Add(new ScriptBundle("~/bundles/details").Include(
                        "~/Scripts/bluejeans/details/lib.js"));

            bundles.Add(new ScriptBundle("~/bundles/videos").Include(
                        "~/Scripts/bluejeans/videos/accordion.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment/min/moment.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/tinymce/tinymce.min.js").Include(
                        "~/Scripts/tinymce/tinymce.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery.datetimepicker.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bluejeans.css"));
        }
    }
}
