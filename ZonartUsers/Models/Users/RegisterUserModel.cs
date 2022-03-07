using System.ComponentModel.DataAnnotations;
using ZonartUsers.Data;

namespace ZonartUsers.Models.Users
{
    using static GlobalConstants;
    public class RegisterUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(PassMaxLength)]
        [MinLength(PassMinLength)]
        public string Password { get; set; }

        [Required]
        [MaxLength(PassMaxLength)]
        [MinLength(PassMinLength)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string FullName { get; set; }
    }

}
