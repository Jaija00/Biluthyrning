using Biluthyrning.Models;
using Microsoft.EntityFrameworkCore;
using Biluthyrning.ViewModels;

namespace Biluthyrning.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Car> Cars { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Biluthyrning.ViewModels.AvailableCarsViewModel> AvailableCarsViewModel { get; set; } = default!;
	}
}
