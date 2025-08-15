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
	}
}
