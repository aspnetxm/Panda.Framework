using System.Web;
using System.Web.Optimization;

namespace Panda.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //登录
            bundles.Add(new StyleBundle("~/css/login").Include(
                    "~/Content/font.css",
                    "~/Content/panda-login.css"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/cookie").Include("~/Scripts/jquery.cookie*"));
            bundles.Add(new ScriptBundle("~/bundles/md5").Include("~/Scripts/md5/jquery.md5*"));
        }
    }
}
