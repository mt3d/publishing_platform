namespace backend.Models
{
	public class Article
	{
		public int ArticleId { get; set; }
		public string? Slug { get; set; }
		public string? Title { get; set; }
		public string? Subtitle { get; set; }
		public string? Body { get; set; }
		public User? Author { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// TODO: Add tags, comments, favorites count, and the people who added this article as a favorite.
	}
}
