using System.Security.Claims;

namespace backend.Infrastructure.UserManagement
{
	public class UserAccessor : IUserAccessor
	{
		private HttpContextAccessor contextAccessor;

		public UserAccessor(HttpContextAccessor contextAccessor)
		{
			this.contextAccessor = contextAccessor;
		}

		public string? GetCurrentUsername()
		{
			/*
			 * The list of claims come from authentication, e.g., cookies, JWT tokens, etc.
			 * NameIdentifier is the standard claim type for a user’s unique ID (username,
			 * user ID, or subject in a JWT).
			 */
			return contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		}
	}
}
