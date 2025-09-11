namespace frontend.Models
{
	public class Author
	{
		public string Username { get; set; } = string.Empty;
		public string Bio { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public bool Following { get; set; }

		public string Url { get; set; } = string.Empty;
		public string FullName { get; set; } = string.Empty;
	}

	public class Article
	{
		public string Slug { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<string> Tags { get; set; } = new();
		public DateTime CreateAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool Favorited { get; set; }
		public int FovoritesCount { get; set; }
		public string Image { get; set; } = string.Empty;

		public Author Author { get; set; } = new();
		public Publication Publication { get; set; } = new();

		public int ClapsCount { get; set; }
		public int CommentsCount { get; set; }
	}

	public class Publication
	{
		public string Name { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
	}

	public class ArticleResponse
	{
		public List<Article> Articles { get; set; } = new();
		public int ArticlesCount { get; set; }
	}
}
