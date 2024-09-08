using System.ComponentModel.DataAnnotations;

namespace DernSupportBackEnd.Models.DTO
{
    public class RegisterModelDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full Name must be between 2 and 100 characters.", MinimumLength = 2)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
