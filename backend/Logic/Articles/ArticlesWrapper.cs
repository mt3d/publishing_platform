using backend.Models;

namespace backend.Logic.Articles
{
	public class ArticlesWrapper
	{
		public List<Article> Articles { get; set; } = new();
		public int Count { get; set; }
	}
}
