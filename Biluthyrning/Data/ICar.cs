using Biluthyrning.Models;
using System.Runtime.InteropServices;

namespace Biluthyrning.Data
{
	public interface ICar
	{
		Car GetById(int id);
		IEnumerable<Car> GetAll();
		Car Create(Car car);
		void Delete(int id);
		Car Update(Car car);
	}
}
