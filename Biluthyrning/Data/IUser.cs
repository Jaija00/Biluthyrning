using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public interface IUser
	{
		Task<User> GetByIdAsync(int id);
		Task<IEnumerable<User>> GetAllAsync();
		Task<User> CreateAsync(User user);
		Task DeleteAsync(int id);
		Task<User> UpdateAsync(User user);
	}
}
