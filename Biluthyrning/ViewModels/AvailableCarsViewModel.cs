using Biluthyrning.Models;

namespace Biluthyrning.ViewModels
{
    public class AvailableCarsViewModel
    {
        public int Id { get; set; }
        public Booking Booking { get; set; }
        public DatePicker DatePicker { get; set; }

    }
}
