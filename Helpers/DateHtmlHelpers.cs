using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condorcet.B2.AspnetCore.MVC.Application.Helpers;

public static class DateHtmlHelpers
{
    public static IHtmlContent DateFormat(this IHtmlHelper helper, DateTime? dateTime)
    {
        if (!dateTime.HasValue)
            return new HtmlString("Aucune date");
        return new HtmlString(dateTime.Value.ToString("d"));
    }
}