using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
    public class VerifyCodeViewModel
    {
        public string Email { get; set; } = null!;

        [Display(Name = "Verification Code", Prompt = "Verification code")]
        [Required(ErrorMessage = "Enter your verification code")]
        public string Code { get; set; } = null!;
    }
}
