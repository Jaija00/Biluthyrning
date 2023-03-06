namespace Biluthyrning.Models
{
	public class User
	{
		public int UserId { get; set; }
		public bool Blacklist { get; set; }
		public bool IsAdmin { get; set; }
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string Email { get; set; } = "";
		public string PhoneNumber { get; set; } = "";
	}
}
