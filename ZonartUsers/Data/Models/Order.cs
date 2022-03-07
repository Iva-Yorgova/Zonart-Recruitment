using System;
using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Data.Models
{
    using static GlobalConstants;
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        public int TemplateId { get; set; }
        public Template Template { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

    }
}
