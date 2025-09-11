using frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace frontend.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpClientFactory clientFactory;

		public int PageSize = 10;

		public HomeController(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		public IActionResult Index()
		{
			if (User.Identity?.IsAuthenticated == true)
			{
				return RedirectToAction("Feed");
			}

			return View();
		}

		/*
            {
              "articles":[
                {
                ...
                "author": {
                  "username": "jake",
                  "bio": "I work at statefarm",
                  "image": "https://i.stack.imgur.com/xHWG8.jpg",
                  "following": false
                    }
                },
                {
                    ...
                }
                }],
              "articlesCount": 2
            }
		 */
		[Authorize]
		public async Task<IActionResult> Feed(int page = 1)
		{
			var client = clientFactory.CreateClient("ApiClient");
			var response = await client.GetAsync($"/articles?limit={PageSize}&offset={(page - 1) * PageSize}");

			if (!response.IsSuccessStatusCode)
			{
				ViewBag.Error = "Error while loading articles";
				return View(new FeedViewModel());
			}

			var json = await response.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

			var articleResponse = JsonSerializer.Deserialize<ArticleResponse>(json, options);

			FeedViewModel feed = new()
			{
				Articles = articleResponse.Articles,
				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = articleResponse.ArticlesCount
				}
			};

			return View(feed);
		}
	}
}