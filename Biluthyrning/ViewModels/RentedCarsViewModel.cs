using System.ComponentModel;

namespace Biluthyrning.ViewModels
{
    public class RentedCarsViewModel
    {
        public int CarId { get; set; }
        [DisplayName("Modell")]
        public string Name { get; set; } = "";
        [DisplayName("Märke")]
        public string Brand { get; set; } = "";
        [DisplayName("Färg")]
        public string Color { get; set; } = "";
        [DisplayName("Växeltyp")]
        public string Gear { get; set; }
        [DisplayName("Drivmedel")]
        public string FuelType { get; set; } = "";
        [DisplayName("Storlek")]
        public string Size { get; set; } = "";
        [DisplayName("Pris")]
        public int Price { get; set; }
        [DisplayName("Förnamn")]
        public string FirstName { get; set; } = "";
        [DisplayName("Efternamn")]
        public string LastName { get; set; } = "";
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

     
      
    }
}
    
