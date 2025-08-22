using System.Text.Json.Serialization;

namespace backend.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Email { get; set; }
		public string? Bio { get; set; }
		public string? ProfilePic { get; set; }

		// TODO: Add followers, following, and favorite articles
		[JsonIgnore]
		public List<FollowMapping> Followers { get; init; } = new();

		[JsonIgnore]
		public List<FollowMapping> Followees { get; init; } = new();
	}
}
