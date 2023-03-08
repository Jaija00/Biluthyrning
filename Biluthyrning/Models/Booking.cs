using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;

namespace Biluthyrning.Models
{
	public class Booking
	{
		public int Id { get; set; }
		[DisplayName("Registreringsnummer")]
        public int CarId { get; set; }
      
        public int UserId { get; set; }
        [DisplayName("Startdatum")]
        public DateTime Start { get; set; }
        [DisplayName("Slutdatum")]
        public DateTime End { get; set; }
	}
}
