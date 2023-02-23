using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public class BookingRepository : IBooking
	{
		private readonly ApplicationDbContext applicationDbContext;

		public BookingRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public Booking Create(Booking carUser)
		{
			applicationDbContext.Bookings.Add(carUser);
			applicationDbContext.SaveChanges();
			return carUser;
		}

		public void Delete(int id)
		{
			var carUser = applicationDbContext.Bookings.Find(id);
			applicationDbContext.Remove(carUser);
			applicationDbContext.SaveChanges();
		}

		public IEnumerable<Booking> GetAll()
		{
			return applicationDbContext.Bookings.OrderBy(x => x.Id);
		}

		public Booking GetById(int id)
		{
			return applicationDbContext.Bookings.FirstOrDefault(x => x.Id == id);
		}

		public Booking Update(Booking carUser)
		{
			applicationDbContext.Update(carUser);
			applicationDbContext.SaveChanges();
			return carUser;
		}
	}
}
