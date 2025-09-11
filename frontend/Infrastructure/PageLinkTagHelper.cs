using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace frontend.Infrastructure
{
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper
	{
		private IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
		{
			this.urlHelperFactory = urlHelperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext? ViewContext { get; set; }

		public PagingInfo? PageModel { get; set; }
		public string? PageAction { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (ViewContext != null && PageModel != null)
			{
				IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

				TagBuilder result = new TagBuilder("div");

				for (int i = 1; i <= PageModel.TotalPages; i++)
				{
					TagBuilder link = new TagBuilder("a");
					link.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
					link.InnerHtml.Append(i.ToString());

					result.InnerHtml.AppendHtml(link);
				}

				output.Content.AppendHtml(result.InnerHtml);
			}
		}
	}
}
