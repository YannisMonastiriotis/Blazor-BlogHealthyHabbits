using System.ComponentModel.DataAnnotations;

namespace HealthyHabbitsWeb.Data.ViewModels
{
    public static class RegexPatterns
    {
        public const string Email = @"^[A-Za-z]\\w{5, 29}$";
    }
    public class SignUpViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        //[RegularExpression(RegexPatterns.Email, ErrorMessage = "Username can only contain alphanumeric characters.")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
       // [RegularExpression(RegexPatterns.Email, ErrorMessage = "Username can only contain alphanumeric characters.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Must be between 8 and 50 characters", MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        public string? Role { get; set; } = "user";
    }
}
