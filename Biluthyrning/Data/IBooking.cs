using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public interface IBooking
	{
		Task<Booking> GetById(int id);
		IEnumerable<Booking> GetAll();
		Task<Booking> Create(Booking booking);
		Task Delete(int id);
		Task<Booking> Update(Booking booking);
	}
}
