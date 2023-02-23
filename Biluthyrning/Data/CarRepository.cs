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
		public async Task<Car> Create(Car car)
		{
			applicationDbContext.Cars.Add(car);
			await applicationDbContext.SaveChangesAsync();
			return car;
		}

		public async Task Delete(int id)
		{
			var car = applicationDbContext.Cars.Find(id);
			applicationDbContext.Cars.Remove(car);
			await applicationDbContext.SaveChangesAsync();
		}

		public IEnumerable<Car> GetAll()
		{
			return applicationDbContext.Cars.OrderBy(c => c.Size).ThenBy(c => c.Manual);
		}

		public async Task<Car> GetById(int id)
		{
			return await applicationDbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Car> Update(Car car)
		{
			applicationDbContext.Update(car);
			await applicationDbContext.SaveChangesAsync();
			return car;
		}
	}
}
