using backend.Data;
using backend.Data.Entities;
using backend.Infrastructure.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Logic
{
	[Route("articles")]
	public class CommentsController : ControllerBase
	{
		private PlatformContext context;
		private IUserAccessor userAccessor;

		public CommentsController(PlatformContext context, IUserAccessor userAccessor)
		{
			this.context = context;
			this.userAccessor = userAccessor;
		}

		/*
		 * POST /api/articles/:slug/comments
		 * 
		 *	{
		 *		"comment": {
		 *			"body": "His name was my name too."
		 *		}
		 *	}
		 *	
		 *	Authentication required, returns the created Comment
		 *	Required field: body
		 */
		[HttpPost("{slug}/comments")]
		[Authorize]
		public async Task<IActionResult> Create(string slug, [FromBody] string body)
		{
			Article? article = await context.Articles.Include(a => a.Comments).FirstOrDefaultAsync(a => a.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			User author = await context.Users.FirstAsync(u => u.Username == userAccessor.GetCurrentUsername());

			Comment comment = new Comment
			{
				Author = author,
				Article = article,
				Text = body,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await context.Comments.AddAsync(comment);
			await context.SaveChangesAsync();

			return Ok(comment);
		}

		[HttpGet("{slug}/comments")]
		public async Task<IActionResult> Get(string slug)
		{
			Article? article = await context
				.Articles.Include(a => a.Comments).ThenInclude(c => c.Author)
				.FirstOrDefaultAsync(a => a.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			return Ok(article.Comments);
		}

		[HttpDelete("{slug}/comments/{id}")]
		public async Task<IActionResult> Delete(string slug, int id)
		{
			Article? article = await context
				.Articles.Include(a => a.Comments)
				.FirstOrDefaultAsync(a => a.Slug == slug);

			Comment? comment = article?.Comments.FirstOrDefault(c => c.Id == id);

			if (comment == null)
			{
				return NotFound();
			}

			context.Comments.Remove(comment);
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
