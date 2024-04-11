using Brickwell.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Brickwell.Data.ViewModels;
using System.Drawing.Printing;

namespace Brickwell.Data.ViewModels

{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]

        public ViewContext? ViewContext { get; set; }
        public string? PageAction { get; set; }
        public string? CurrentColor { get; set; }
        public string? CurrentCategory { get; set; }
        public string? DesiredPageSize { get; set; }
        public PaginationInfo PageModel { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; } = String.Empty;
        public string PageClassNormal { get; set; } = String.Empty;
        public string PageClassSelected { get; set; } = String.Empty;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                for (int i = 1; i <= PageModel.TotalNumPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i , primaryColor = CurrentColor, category = CurrentCategory, pageSize = DesiredPageSize });

                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }


                    tag.InnerHtml.Append(i.ToString());

                    result.InnerHtml.AppendHtml(tag);
                    result.InnerHtml.AppendHtml(" ");
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }

    }
}
