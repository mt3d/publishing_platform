using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Logic
{
	public class PlatformContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<Article> Articles => Set<Article>();

		// TODO: Handle transaction
	}
}
