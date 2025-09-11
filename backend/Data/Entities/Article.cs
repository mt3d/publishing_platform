﻿using System.Text.Json.Serialization;

namespace backend.Data.Entities
{
	public class Article
	{
		// TODO: Use long or Guid?
		// TODO: Rename to Id?
		public int ArticleId { get; set; }

		public string? Slug { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; } // Subtitle

		// TODO: A body is not just a string. There are complex formatting and links.
		public string? Body { get; set; }
		public User? Author { get; set; }

		public List<Comment> Comments { get; set; } = new();

		public List<string> Tags { get; set; } = new();
		public bool Favorited { get; set; } // Dynamically generated
		public int FovoritesCount { get; set; } // Dynamically generated
		public string Image { get; set; } = string.Empty;
		public int ClapsCount { get; set; } // Dynamically generated
		public int CommentsCount { get; set; } // Dynamically generated

		public Publication? Publication { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// TODO: Add tags, comments, favorites count, and the people who added this article as a favorite.
	}
}
