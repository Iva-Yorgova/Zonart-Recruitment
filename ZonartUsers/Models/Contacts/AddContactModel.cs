using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Contacts
{
    using static Data.GlobalConstants;
    public class AddContactModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(MessageMinLength)]
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
