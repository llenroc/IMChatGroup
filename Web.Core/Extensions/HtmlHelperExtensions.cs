using System.Web.Mvc;
using Web.Core.Helpers;

namespace Web.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static UserHtmlHelper User(this HtmlHelper html)
        {
            return new UserHtmlHelper(html, new UrlHelper(html.ViewContext.RequestContext));
        }
    }
}