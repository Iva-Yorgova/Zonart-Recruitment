
using System;
using System.ComponentModel.DataAnnotations;


namespace ZonartUsers.Data.Models
{
    using static DataConstants;

    public class JobSkill
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }


        public string JobId { get; set; }
        public Job Job { get; set; }
    }
}
