using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Condorcet.B2.AspnetCore.MVC.Application.Helpers;

[HtmlTargetElement("strong-text")]
public class StrongTextHelper: TagHelper
{
    [HtmlAttributeName("text")]
    public string Text { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "strong";
        output.Content.SetContent(Text);
    }
}