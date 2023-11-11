using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OrderApp.TagHelpers
{
    [HtmlTargetElement("input")]
    [HtmlTargetElement("button")]
    [HtmlTargetElement("select")]
    public class SetDisabledValueTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-is-disabled")]
        public bool IsDisabled { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsDisabled)
            {
                var d = new TagHelperAttribute("disabled");
                output.Attributes.Add(d);
            }
            base.Process(context, output);
        }
    }
}
