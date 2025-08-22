namespace backend.Infrastructure.UserManagement
{
	/*
	 * A user accessor is used throughout the Api to get the current user.
	 * 
	 * This is cleaner than using HttpContext.User directly.
	 * Another alternative is UserManager<TUser> if we plan to use
	 * ASP.NET Core Identity.
	 */
	public interface IUserAccessor
	{
		string? GetCurrentUsername();
	}
}
