using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public interface IBooking
	{
		Task<Booking> GetByIdAsync(int id);
		Task<IEnumerable<Booking>> GetAllAsync();
		Task<Booking> CreateAsync(Booking booking);
		Task DeleteAsync(int id);
		Task<Booking> UpdateAsync(Booking booking);
	}
}
