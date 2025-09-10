using System.Text.Json.Serialization;

namespace backend.Data.Entities
{
	/*	{
	  "comment": {
		"id": 1,
		"createdAt": "2016-02-18T03:22:56.637Z",
		"updatedAt": "2016-02-18T03:22:56.637Z",
		"body": "It takes a Jacobian",
		"author": {
		  "username": "jake",
		  "bio": "I work at statefarm",
		  "image": "https://i.stack.imgur.com/xHWG8.jpg",
		  "following": false
		}
	  }
	}*/

	public class Comment
	{
		public int Id { get; set; }

		public string? Text { get; set; }

		public User? Author { get; set; }

		// Just return the author above.
		// Efficient for filtering.
		[JsonIgnore]
		public int AuthorId { get; set; }

		// Efficient for filtering. No need to include the article.
		[JsonIgnore]
		public int ArticleId { get; set; }

		// When returning comments, do not return the full article.
		[JsonIgnore]
		public Article? Article { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
