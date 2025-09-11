namespace frontend.Models
{
	public class FeedViewModel
	{
		public IEnumerable<Article> Articles { get; set; } = Enumerable.Empty<Article>();
		public PagingInfo PagingInfo { get; set; } = new();
	}

	public class PagingInfo
	{
		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
		public int CurrentPage { get; set; }

		public int TotalPages => TotalItems != 0
			? (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage)
			: 0;
	}
}
