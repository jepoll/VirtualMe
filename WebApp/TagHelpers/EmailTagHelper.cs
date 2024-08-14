using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers;

public class EmailTagHelper: TagHelper
{
    public string? Domain { get; set; }
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // replace original tag
        output.TagName = "a"; 

        // get the original tag helper content
        var content = await output.GetChildContentAsync();

        var address = (content.GetContent() + "@" + Domain).ToLower();

        output.Attributes.SetAttribute("href", "mailto:" + address);
        output.Content.SetContent("Mail to: " + address);
    }
}
