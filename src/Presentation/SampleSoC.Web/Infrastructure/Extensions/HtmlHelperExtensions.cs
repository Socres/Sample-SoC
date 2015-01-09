namespace SampleSoC.Web.Infrastructure.Extensions
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Helper class for transforming Markdown.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Transforms a string of Markdown into HTML.
        /// </summary>
        /// <param name="helper">HtmlHelper - Not used, but required to make this an extension method.</param>
        /// <param name="text">The Markdown that should be transformed.</param>
        /// <returns>
        /// The HTML representation of the supplied Markdown.
        /// </returns>
        public static IHtmlString Markdown(this HtmlHelper helper, string text)
        {
            var markdownService = new Kiwi.Markdown.MarkdownService(null);
            var html = markdownService.ToHtml(text);
            return new MvcHtmlString(html);
        }

        /// <summary>
        /// Transforms a string of Markdown into an exerpt.
        /// </summary>
        /// <param name="helper">HtmlHelper - Not used, but required to make this an extension method.</param>
        /// <param name="text">The Markdown that should be transformed.</param>
        /// <param name="length">The length.</param>
        /// <returns>
        /// The HTML representation of the supplied Markdown.
        /// </returns>
        public static IHtmlString MarkdownExcerpt(this HtmlHelper helper, string text, int length)
        {
            var markdownService = new Kiwi.Markdown.MarkdownService(null);
            var html = markdownService.ToHtml(text);
            // Replace all line breaks so we can maintain them after creating the excerpt
            var ret = html.Replace("<br>", Environment.NewLine).Replace("<br />", Environment.NewLine);
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var plainText = HttpUtility.HtmlDecode(reg.Replace(ret, ""));

            //Make sure we don't have multiple line breaks
            while (plainText.IndexOf(Environment.NewLine + Environment.NewLine, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                plainText = plainText.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
            }
            // Restore all line breaks
            plainText = plainText.Replace(Environment.NewLine, "<br />");

            var trimmed = plainText;
            if (trimmed.Length > (length - 5))
            {
                trimmed = trimmed.Substring(0, length) + "(...)";
            }

            return new MvcHtmlString(trimmed);
        }
    }
}