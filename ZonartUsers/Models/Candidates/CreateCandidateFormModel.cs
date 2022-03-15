using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Models.Candidates
{
    public class CreateCandidateFormModel
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

        [Required]
        public string Skill { get; set; }

        [Required]
        public IEnumerable<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();

        [Required]
        public string RecruiterName { get; set; }

        [Required]
        public string RecruiterEmail { get; set; }

        [Required]
        public string RecruiterCountry { get; set; }
    }
}
