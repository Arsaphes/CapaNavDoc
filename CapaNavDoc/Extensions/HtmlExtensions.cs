using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CapaNavDoc.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString HtmlActionLink(this AjaxHelper helper, string html, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            string link = helper.ActionLink("[replace]", actionName, routeValues, ajaxOptions, htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("[replace]", html));
        }
    }
}
