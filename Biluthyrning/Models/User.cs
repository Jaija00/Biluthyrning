namespace Biluthyrning.Models
{
	public class User
	{
		public int Id { get; set; }
		public bool Blacklist { get; set; }
		public bool IsAdmin { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
}
