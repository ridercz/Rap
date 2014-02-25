using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Altairis.Rap {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include("~/Scripts/modernizr-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jqui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jqext").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js","~/Scripts/jquery.ui.timepicker.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Site/js").Include("~/Scripts/site/*.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));

        }

    }
}