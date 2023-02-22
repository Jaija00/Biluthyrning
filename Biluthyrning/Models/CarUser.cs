namespace Biluthyrning.Models
{
	public class CarUser
	{
		public int Id { get; set; }
		public int CarId { get; set; }
		public int UserId { get; set; }
		public DateTime Booked { get; set; }
	}
}
