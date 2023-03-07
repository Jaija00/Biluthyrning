namespace Biluthyrning.ViewModels
{
    public class DetailsUserViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

    }
}
