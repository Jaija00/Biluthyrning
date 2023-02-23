using Biluthyrning.Models;
using System.Runtime.InteropServices;

namespace Biluthyrning.Data
{
	public interface ICar
	{
		Task<Car> GetById(int id);
		IEnumerable<Car> GetAll();
		Task<Car> Create(Car car);
		Task Delete(int id);
		Task<Car> Update(Car car);
	}
}
