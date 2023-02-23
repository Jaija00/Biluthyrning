using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public interface IBooking
	{
		Booking GetById(int id);
		IEnumerable<Booking> GetAll();
		Booking Create(Booking carUser);
		void Delete(int id);
		Booking Update(Booking carUser);
	}
}
