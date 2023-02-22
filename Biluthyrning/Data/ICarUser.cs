using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public interface ICarUser
	{
		CarUser GetById(int id);
		IEnumerable<CarUser> GetAll();
		CarUser Create(CarUser carUser);
		void Delete(int id);
		CarUser Update(CarUser carUser);
	}
}
