using Biluthyrning.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biluthyrning.ViewModels
{
    public class DetailsUserViewModel
    {
        public Booking Booking { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
       

    }
}
