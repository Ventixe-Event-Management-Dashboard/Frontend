using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
    public class RegisterEmailViewModel
    {
        [EmailAddress]
        [Display(Name = "Email", Prompt = "example@email.com")]
        [Required(ErrorMessage = "Enter a valid email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = null!;
    }
}
