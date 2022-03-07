using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Orders
{
    using static Data.GlobalConstants;
    public class OrderTemplateModel
    {
        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

    }

}

