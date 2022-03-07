
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ZonartUsers.Data.Models
{
    using static DataConstants;

    public class Interview
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Date { get; set; }

        [Required]
        public string RecruiterName { get; set; }

        [Required]
        public string CandidateName { get; set; }

        [Required]
        public string JobName { get; set; }

    }
}
