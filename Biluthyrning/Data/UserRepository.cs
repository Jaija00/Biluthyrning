using Biluthyrning.Models;

namespace Biluthyrning.Data
{
	public class UserRepository : IUser
	{
		private readonly ApplicationDbContext applicationDbContext;

		public UserRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public User Create(User user)
		{
			applicationDbContext.Users.Add(user);
			applicationDbContext.SaveChanges();
			return user;
		}

		public void Delete(int id)
		{
			var user = applicationDbContext.Users.Find(id);
			applicationDbContext.Users.Remove(user);
			applicationDbContext.SaveChanges();
		}

		public IEnumerable<User> GetAll()
		{
			return applicationDbContext.Users.OrderBy(x => x.FirstName);
		}

		public User GetById(int id)
		{
			return applicationDbContext.Users.FirstOrDefault(x => x.Id == id);
		}

		public User Update(User user)
		{
			applicationDbContext.Update(user);
			applicationDbContext.SaveChanges();
			return user;
		}
	}
}
