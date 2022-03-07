using System.ComponentModel.DataAnnotations;
using ZonartUsers.Data;

namespace ZonartUsers.Models.Users
{
    using static GlobalConstants;
    public class LoginUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(PassMaxLength)]
        [MinLength(PassMinLength)]
        public string Password { get; set; }


        public string FullName { get; set; }
    }
}
