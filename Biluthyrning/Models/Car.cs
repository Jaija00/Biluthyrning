namespace Biluthyrning.Models
{
	public class Car
	{
		public int CarId { get; set; }
		public string Name { get; set; } = "";
		public string Brand { get; set; } = "";
		public string Color { get; set; } = "";
		public bool Manual { get; set; }
		public string FuelType { get; set; } = "";
		public string Size { get; set; } = "";
		public int Price { get; set; }
	}
}
