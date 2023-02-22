using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public class CarUserRepository : ICarUser
	{
		private readonly ApplicationDbContext applicationDbContext;

		public CarUserRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public CarUser Create(CarUser carUser)
		{
			applicationDbContext.CarUsers.Add(carUser);
			applicationDbContext.SaveChanges();
			return carUser;
		}

		public void Delete(int id)
		{
			var carUser = applicationDbContext.CarUsers.Find(id);
			applicationDbContext.Remove(carUser);
			applicationDbContext.SaveChanges();
		}

		public IEnumerable<CarUser> GetAll()
		{
			return applicationDbContext.CarUsers.OrderBy(x => x.Id);
		}

		public CarUser GetById(int id)
		{
			return applicationDbContext.CarUsers.FirstOrDefault(x => x.Id == id);
		}

		public CarUser Update(CarUser carUser)
		{
			applicationDbContext.Update(carUser);
			applicationDbContext.SaveChanges();
			return carUser;
		}
	}
}
