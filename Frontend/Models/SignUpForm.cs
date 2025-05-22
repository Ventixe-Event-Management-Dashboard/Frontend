using System.ComponentModel.DataAnnotations;

namespace Frontend.Models;

public class SignUpForm
{

    [Display(Name = "First Name", Prompt = "John")]
    [Required(ErrorMessage = "Enter your first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Doe")]
    [Required(ErrorMessage = "Enter your last name")]
    public string LastName { get; set; } = null!;

    [EmailAddress]
    [Display(Name = "Email", Prompt = "example@email.com")]
    [Required(ErrorMessage = "Enter a valid email")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Password")]
    [Required(ErrorMessage = "Enter a valid password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
    ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, digit, and special character.")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
    [Required(ErrorMessage = "Confirm your password")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;

}