using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using backend.Infrastructure.UserManagement;

namespace backend.Logic.Users
{
	[Route("user")]
	public class UserController(PlatformContext context, IUserAccessor accessor, IMapper mapper) : Controller
	{
		// TODO: Authentication required.
		[HttpGet]
		public async Task<IActionResult> GetCurrent()
		{
			var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == accessor.GetCurrentUsername());

			if (user == null)
			{
				return NotFound(StatusCodes.Status404NotFound);
			}

			var userDto = mapper.Map<User, UserDto>(user);
			return Ok(user);
		}

		// TODO: Authentication required
		[HttpPut]
		public async Task<IActionResult> UpdateUser(
			[FromBody] string username,
			[FromBody] string email,
			[FromBody] string password,
			[FromBody] string bio,
			[FromBody] string profilePic)
		{
			var currentUsername = accessor.GetCurrentUsername();
			var user = await context.Users.Where(u => u.Username == currentUsername).FirstOrDefaultAsync();

			if (user == null)
			{
				return NotFound(StatusCodes.Status404NotFound);
			}

			// TODO: Create a user service?

			user.Username = username ?? user.Username;
			user.Email = email ?? user.Email;
			user.Bio = bio ?? user.Bio;
			user.ProfilePic = profilePic ?? user.ProfilePic;

			// TODO: Update password in a separate endpoint

			await context.SaveChangesAsync();

			return Ok(mapper.Map<User, UserDto>(user));
		}
	}
}
