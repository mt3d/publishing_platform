using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Logic.Articles
{
	[Route("[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private PlatformContext context;

		public ArticlesController(PlatformContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Get a list of Articles
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ArticlesWrapper> Get(
			[FromQuery] string tag,
			[FromQuery] string author,
			[FromQuery] int? limit,
			int? offset)
		{
			// TODO: Include Authors, ArticleFavorites, and Article Tags
			var query = context.Articles.AsNoTracking();

			var articles = await query
				.OrderByDescending(a => a.CreatedAt)
				.Skip(offset ?? 0)
				.Take(limit ?? 0)
				.AsNoTracking()
				.ToListAsync();

			// TODO: Handle tags
			// TODO: Handle author

			return new ArticlesWrapper { Articles = articles, Count = query.Count() };
		}

		[HttpGet("{slug}")]
		public async Task<IActionResult> Get(string slug)
		{
			var article = await context.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			return Ok(new ArticleWrapper(article));
		}
	}
}
