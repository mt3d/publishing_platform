using AutoMapper;
using backend.Data;
using backend.Data.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace backend.Logic.Users
{
	// TODO: Consider creating an user repository that handles the authentication logic.

	//[ApiController]
	[Route("authentication")]
	public class AuthenticationController : ControllerBase
	{
		private PlatformContext context; // TODO: Replace with a user service
		private IMapper mapper;

		public AuthenticationController(PlatformContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public record UserData(string? Username, string? Email, string? Password);
		public record UserCredentials(string? Email, string? Password);

		/*
		 * Expected request body:
		 * {
		 *		"username": "test_name",
		 *		"email": "test@test_provider.com",
		 *		"password": "12345678"
		 * }
		 */
		// TODO: Add validation for all fields (password should not be empty)
		// TODO: Convert emails to lower case
		// No need for authorization, since this is a public website.
		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromBody] UserData data)
		{
			// Validation: Ensure the username is not used => TODO: move to client-validation or remote validation
			if (await context.Users.Where(u => u.Username == data.Username).AnyAsync())
			{
				// TODO: Return a descriptive message
				return BadRequest();
			}

			// Validation: Ensure the email is not used => TODO: move somewhere else.
			if (await context.Users.Where(u => u.Email == data.Email).AnyAsync())
			{
				return BadRequest();
			}

			// TODO: Study how to generate a radnom salt
			byte[] salt = Guid.NewGuid().ToByteArray();

			Data.Entities.User user = new User
			{
				Username = data.Username,
				Email = data.Email,
				HashedPassword = CreateHashCode(data.Password, salt),
				Salt = salt
			};

			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();

			return Ok(user);
		}

		[HttpPost("signin")]
		public async Task<IActionResult> SignIn([FromBody] UserCredentials data)
		{
			var user = await context.Users.Where(u => u.Email == data.Email).SingleOrDefaultAsync();

			if (user == null)
			{
				return NotFound();
			}

			// TODO: Considere extracting into Repository.ValidateCredentials()

			var hash = CreateHashCode(data.Password, user.Salt);

			// Compare in constant time to prevent timing attacks.
			bool areEqual = CryptographicOperations.FixedTimeEquals(user.HashedPassword, hash);

			if (!areEqual)
			{
				return Unauthorized();
			}

			var userDto = mapper.Map<User, UserDto>(user);

			// TODO: Generate JWT token
			userDto.Token = CreateJwtToken(user.Username);

			return Ok(userDto);
		}

		// TODO: extract into a utlity class
		private byte[] CreateHashCode(string password, byte[] salt)
		{
			// byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

			// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
			// You can use Convert.ToBase64String() to convert the hash to a string.
			return Rfc2898DeriveBytes.Pbkdf2(
				password: System.Text.Encoding.UTF8.GetBytes(password),
				salt: salt,
				iterations: 100_000,
				hashAlgorithm: HashAlgorithmName.SHA256,
				outputLength: 32      // 256-bit hash
			);
		}

		// TODO: extract into a utility class
		// The following method is not customizable.
		private string CreateJwtToken(string username)
		{
			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

			var claims = new[]
			{
				// Standard JWT claim that identifies the user (often set to the username, userId, or GUID).
				new Claim(JwtRegisteredClaimNames.Sub, username),

				// (JWT ID) A unique identifier for this token, usually a GUID or random string.
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

				// (Issued At) Stored as Unix timestamp (seconds since 1970).
				new Claim(
					JwtRegisteredClaimNames.Iat,
					new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
					ClaimValueTypes.Integer64
				),
			};

			// Alternative:
			// var signingKey = new SymmetricSecurityKey("jwt_secret"u8.ToArray());

			JwtSecurityToken token = new JwtSecurityToken(
				"issuer",
				"audience",
				claims,
				DateTime.UtcNow,
				DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
				new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("a_long_enough_secret_for_this_algorithm")), SecurityAlgorithms.HmacSha256Signature)
			);

			string compactJwt = handler.WriteToken(token);
			return compactJwt;
		}
	}
}
