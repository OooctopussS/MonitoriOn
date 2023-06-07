using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Primitives;

namespace MonitoriOn.Common
{
    public static class HtmlPrefixScopeExtensions
    {
        private const string IdsToReuseKey = "__htmlPrefixScopeExtensions_IdsToReuse_";
        public static IDisposable BeginCollectionItem(this IHtmlHelper html, string collectionName)
        {
            return BeginCollectionItem(html, collectionName, html.ViewContext.Writer);
        }
        public static IDisposable BeginCollectionItem(this IHtmlHelper html, string collectionName, TextWriter writer)
        {
            if (html.ViewData["ContainerPrefix"] != null)
                collectionName = string.Concat(html.ViewData["ContainerPrefix"], ".", collectionName);

            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
            var itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();
            string htmlFieldPrefix = $"{collectionName}[{itemIndex}]";
            html.ViewData["ContainerPrefix"] = htmlFieldPrefix;

            string indexInputName = $"{collectionName}.index";
            writer.WriteLine($@"<input type=""hidden"" name=""{indexInputName}"" autocomplete=""off"" value=""{html.Encode(itemIndex)}"" />");
            return BeginHtmlFieldPrefixScope(html, htmlFieldPrefix);
        }
        public static IDisposable BeginHtmlFieldPrefixScope(this IHtmlHelper html, string htmlFieldPrefix)
        {
            return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
        }
        private static Queue<string> GetIdsToReuse(HttpContext httpContext, string collectionName)
        {
            var key = IdsToReuseKey + collectionName;
            if (httpContext.Items[key] is not Queue<string> queue)
            {
                httpContext.Items[key] = queue = new Queue<string>();
                if (httpContext.Request.Method == "POST" && httpContext.Request.HasFormContentType)
                {
                    StringValues previouslyUsedIds = httpContext.Request.Form[collectionName + ".index"];
                    if (!string.IsNullOrEmpty(previouslyUsedIds))
                        foreach (var previouslyUsedId in previouslyUsedIds)
                            queue.Enqueue(previouslyUsedId);
                }
            }
            return queue;
        }
        internal class HtmlFieldPrefixScope : IDisposable
        {
            internal readonly TemplateInfo TemplateInfo;
            internal readonly string PreviousHtmlFieldPrefix;
            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
            {
                TemplateInfo = templateInfo;
                PreviousHtmlFieldPrefix = TemplateInfo.HtmlFieldPrefix;
                TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            }
            public void Dispose()
            {
                TemplateInfo.HtmlFieldPrefix = PreviousHtmlFieldPrefix;
            }
        }
    }
}
