using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public interface IUser
	{
		User GetById(int id);
		IEnumerable<User> GetAll();
		User Create(User user);
		void Delete(int id);
		User Update(User user);
	}
}
