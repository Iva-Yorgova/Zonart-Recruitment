using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Templates
{
    using static Data.GlobalConstants;
    public class AddTemplateModel
    {
        [Required]
        [StringLength(TemplateNameMaxLength, MinimumLength = TemplateNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(TemplateDescriptionMaxLength, MinimumLength = TemplateDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }


        [Required]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
