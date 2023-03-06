using Biluthyrning.Models;
using Microsoft.EntityFrameworkCore;

namespace Biluthyrning.Data
{
	public class UserRepository : IUser
	{
		private readonly ApplicationDbContext applicationDbContext;

		public UserRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<User> CreateAsync(User user)
		{
			applicationDbContext.Users.Add(user);
			await applicationDbContext.SaveChangesAsync();
			return user;
		}

		public async Task DeleteAsync(int id)
		{
			var user = applicationDbContext.Users.Find(id);
			applicationDbContext.Users.Remove(user);
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await applicationDbContext.Users.OrderBy(x => x.FirstName).ToListAsync();
		}

		public async Task<User> GetByIdAsync(int id)
		{
			return await applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
		}

		public async Task<User> UpdateAsync(User user)
		{
			applicationDbContext.Update(user);
			await applicationDbContext.SaveChangesAsync();
			return user;
		}
	}
}
