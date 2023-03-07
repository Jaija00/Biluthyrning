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
		public async Task<Booking> CreateAsync(Booking booking)
		{
			applicationDbContext.Bookings.Add(booking);
			await applicationDbContext.SaveChangesAsync();
			return booking;
		}

		public async Task DeleteAsync(int id)
		{
			var booking = applicationDbContext.Bookings.Find(id);
			applicationDbContext.Remove(booking);
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Booking>> GetAllAsync()
		{
			return await applicationDbContext.Bookings.OrderBy(x => x.Id).ToListAsync();
		}

		public async Task<Booking> GetByIdAsync(int id)
		{
			return await applicationDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
		}
        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int id)
        {
            return await applicationDbContext.Bookings.Where(x => x.UserId == id).ToListAsync();
        }

        public async Task<Booking> UpdateAsync(Booking booking)
		{
			applicationDbContext.Update(booking);
			await applicationDbContext.SaveChangesAsync();
			return booking;
		}

        public async Task<Booking> AddAsync(Booking booking)
		{
			applicationDbContext.Add(booking);	
			await applicationDbContext.SaveChangesAsync();
			return booking;
		}
        public async Task SaveChangesAsync()
		{
			applicationDbContext.SaveChanges();
		}
    }
}
