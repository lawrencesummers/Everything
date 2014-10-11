using System.Web.Optimization;

namespace BootstrapSupport
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.Bundles.IgnoreList.Clear();


            BundleTable.Bundles.IgnoreList.Ignore(".intellisense.js", OptimizationMode.Always);
            BundleTable.Bundles.IgnoreList.Ignore("-vsdoc.js", OptimizationMode.Always);
            BundleTable.Bundles.IgnoreList.Ignore(".debug.js", OptimizationMode.Always);

            //bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery*",
                "~/Scripts/bootstrap*",
                "~/Scripts/modernizr-{version}.js",
                "~/Scripts/locales/bootstrap-datetimepicker.zh-CN.js",
                "~/Content/Buttons/js/buttons.js",
                "~/Content/kindeditor/kindeditor-all.js",
                "~/Content/kindeditor/lang/zh_CN.js",
                "~/Scripts/sco.*",
                "~/Scripts/messenger/messenger.js",
                "~/Scripts/functions.js"
                ));

            bundles.Add(new StyleBundle("~/content/css").Include(

                "~/Content/bootstrap.css",
                "~/Content/bootstrap*",
                "~/Content/font-awesome.css",
                "~/Content/kindeditor/themes/default/default.css",
                "~/Content/messenger/messenger.css",
                "~/Content/messenger/messenger-theme-future.css",
                "~/Content/body.css"
                ));


            BundleTable.EnableOptimizations = false;    //禁止压缩js，某些js压缩后无法使用
        }
    }
}