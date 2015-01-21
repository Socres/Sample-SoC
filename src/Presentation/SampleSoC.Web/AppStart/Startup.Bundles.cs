namespace SampleSoC.Web.AppStart
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Hosting;
    using System.Web.Optimization;

    /// <summary>
    /// Class for configuring bundling and minification for JS and CSS files.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the bundles.
        /// </summary>
        public void SetupBundles()
        {
            var bundles = BundleTable.Bundles;
            bundles.UseCdn = true;

            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            var libScriptsFiles = GetLibScriptsFiles().ToList();
            AddDefaultBundles(bundles, libScriptsFiles);

            AddAppBundles(bundles);
        }

        private static IEnumerable<string> GetLibScriptsFiles()
        {
            var scriptsFolder = HostingEnvironment.MapPath("~/Scripts/");
            if (string.IsNullOrEmpty(scriptsFolder))
            {
                throw new InvalidOperationException("Scripts-Folder not available.");
            }

            return Directory.GetFiles(scriptsFolder, "*.js", SearchOption.AllDirectories).ToList();
        }

        /// <summary>
        /// Adds the default ignore patterns.
        /// </summary>
        /// <param name="ignoreList">The ignore list.</param>
        /// <exception cref="System.ArgumentNullException">ignoreList is null.</exception>
        private static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }

        /// <summary>
        /// Adds the default bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        /// <param name="scriptFiles">The scripts files.</param>
        private static void AddDefaultBundles(BundleCollection bundles, IEnumerable<string> scriptFiles)
        {
            var scriptFileList = scriptFiles.ToList();

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/jsjquery",
                    "//ajax.aspnetcdn.com/ajax/jQuery/" + GetFileName("jquery-", scriptFileList) + ".min.js")
                {
                    CdnFallbackExpression = "window.$"
                }
                    .Include("~/Scripts/" + GetFileName("jquery-", scriptFileList) + ".js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/jsjquerymigrate",
                    "//ajax.aspnetcdn.com/ajax/jquery.migrate/" + GetFileName("jquery-migrate-", scriptFileList) + ".min.js")
                {
                    CdnFallbackExpression = "window.$.migrateWarnings"
                }
                    .Include("~/Scripts/" + GetFileName("jquery-migrate-", scriptFileList) + ".js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jsbootstrap")
                    .Include("~/Scripts/" + GetFileName("knockout-", scriptFileList) + ".js")
                    .Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/marked.js",
                      "~/Scripts/bootstrap-markdown.js",
                      "~/Scripts/Highlight/highlight.pack.js"));

            bundles.Add(
                new StyleBundle("~/Content/cssbootstrap")
                    .Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap-markdown/bootstrap-markdown.min.css",
                      "~/Content/Highlight/default.css",
                      "~/Content/Highlight/vs.css"));

            bundles.Add(
                new StyleBundle("~/Content/cssmain")
                    .Include("~/Content/site.css"));
        }

        private static void AddAppBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/app")
                    .IncludeDirectory("~/Scripts/app", "*.js"));
        }


        private static string GetFileName(string fileName, IEnumerable<string> jsFiles)
        {
            var regex = new Regex(fileName + @"\d+\.\d+\.\d+\.js$");
            return Path.GetFileNameWithoutExtension(jsFiles.First(regex.IsMatch));
        }
    }
}