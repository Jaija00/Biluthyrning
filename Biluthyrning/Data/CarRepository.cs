using Biluthyrning.Models;
using Microsoft.EntityFrameworkCore;

namespace Biluthyrning.Data
{
	public class CarRepository : ICar
	{
		private readonly ApplicationDbContext applicationDbContext;

		public CarRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Car> CreateAsync(Car car)
		{
			applicationDbContext.Cars.Add(car);
			await applicationDbContext.SaveChangesAsync();
			return car;
		}

		public async Task DeleteAsync(int id)
		{
			var car = applicationDbContext.Cars.Find(id);
			applicationDbContext.Cars.Remove(car);
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Car>> GetAllAsync()
		{
			return await applicationDbContext.Cars.OrderBy(c => c.Size).ThenBy(c => c.Manual).ToListAsync();
		}

		public async Task<Car> GetByIdAsync(int id)
		{
			return await applicationDbContext.Cars.FirstOrDefaultAsync(c => c.CarId == id);
		}

		public async Task<Car> UpdateAsync(Car car)
		{
			applicationDbContext.Update(car);
			await applicationDbContext.SaveChangesAsync();
			return car;
		}
	}
}
