using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
    public class UserAddressViewModel
    {
        [Required(ErrorMessage = "Enter a street address")]
        [Display(Name = "Street", Prompt = "Vägen 1")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Enter a city")]
        [Display(Name = "City", Prompt = "Stockholm")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Enter a postal code")]
        [Display(Name = "Postal Code", Prompt = "12345")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Enter a valid postal code")]
        public string? PostalCode { get; set; }
    }
}


