using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public class CarRepository : ICar
	{
		private readonly ApplicationDbContext applicationDbContext;

		public CarRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public Car Create(Car car)
		{
			applicationDbContext.Cars.Add(car);
			applicationDbContext.SaveChanges();
			return car;
		}

		public void Delete(int id)
		{
			var car = applicationDbContext.Cars.Find(id);
			applicationDbContext.Cars.Remove(car);
			applicationDbContext.SaveChanges();
		}

		public IEnumerable<Car> GetAll()
		{
			return applicationDbContext.Cars.OrderBy(c => c.Size).ThenBy(c => c.Manual);
		}

		public Car GetById(int id)
		{
			return applicationDbContext.Cars.FirstOrDefault(c => c.Id == id);
		}

		public Car Update(Car car)
		{
			applicationDbContext.Update(car);
			applicationDbContext.SaveChanges();
			return car;
		}
	}
}
