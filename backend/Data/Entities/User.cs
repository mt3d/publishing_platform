using System.Text.Json.Serialization;

namespace backend.Data.Entities
{
	public class User
	{
		public int? UserId { get; set; }

		// TODO: Ensure usernames are unique.
		public string? Username { get; set; }

		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? Bio { get; set; }
		public string? ProfilePic { get; set; }
		public string? Url { get; set; }

		// TODO: Add followers, following, and favorite articles
		[JsonIgnore]
		public List<FollowMapping> Followers { get; init; } = new();

		[JsonIgnore]
		public List<FollowMapping> Followees { get; init; } = new();

		// Store a one-way hash code instead of a password.
		[JsonIgnore]
		public byte[] HashedPassword { get; set; } = [];

		/*
		 * Hash algorithms produce the same result every time, which means the same
		 * hash code for all users who chose the same password.
		 * 
		 * A random salt value is added to the password so that users can have the
		 * same password without causing duplicate hash codes.
		 */
		[JsonIgnore]
		public byte[] Salt { get; set; } = [];
	}
}
