using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Enter your first name")]
        [Display(Name = "First Name", Prompt = "John")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Enter your last name")]
        [Display(Name = "Last Name", Prompt = "Doe")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Enter a phone number")]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        [Display(Name = "Phone", Prompt = "123-456-7890")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Enter your address")]
        public UserAddressViewModel Address { get; set; } = new();
    }
}


