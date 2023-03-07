using Microsoft.EntityFrameworkCore.Metadata;

namespace Biluthyrning.Models
{
	public class Booking
	{
		public int Id { get; set; }
		public int CarId { get; set; }
		public int UserId { get; set; }
        public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}
