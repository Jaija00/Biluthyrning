using Biluthyrning.Models;
using System.Runtime.InteropServices;

namespace Biluthyrning.Data
{
	public interface ICar
	{
		Task<Car> GetByIdAsync(int id);
		Task<IEnumerable<Car>> GetAllAsync();
		Task<Car> CreateAsync(Car car);
		Task DeleteAsync(int id);
		Task<Car> UpdateAsync(Car car);
	}
}
