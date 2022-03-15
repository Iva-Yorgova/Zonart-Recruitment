using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Models.Candidates
{
    public class EditCandidateFormModel
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Bio { get; set; }

        [Required]
        public string BirthDate { get; set; }

        public string Skill { get; set; }

        [Required]
        public IEnumerable<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
    }
}
