using Biluthyrning.Models;
using Microsoft.EntityFrameworkCore;

namespace Biluthyrning.Data
{
	public class BookingRepository : IBooking
	{
		private readonly ApplicationDbContext applicationDbContext;

		public BookingRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Booking> Create(Booking booking)
		{
			applicationDbContext.Bookings.Add(booking);
			await applicationDbContext.SaveChangesAsync();
			return booking;
		}

		public async Task Delete(int id)
		{
			var booking = applicationDbContext.Bookings.Find(id);
			applicationDbContext.Remove(booking);
			await applicationDbContext.SaveChangesAsync();
		}

		public IEnumerable<Booking> GetAll()
		{
			return applicationDbContext.Bookings.OrderBy(x => x.Id);
		}

		public async Task<Booking> GetById(int id)
		{
			return await applicationDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Booking> Update(Booking booking)
		{
			applicationDbContext.Update(booking);
			await applicationDbContext.SaveChangesAsync();
			return booking;
		}
	}
}
