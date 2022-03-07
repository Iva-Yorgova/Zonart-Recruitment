﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ZonartUsers.Data.Models
{
    using static DataConstants;

    public class CandidateSkill
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }


        public string CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
