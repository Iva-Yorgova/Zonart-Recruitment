

using System.Collections.Generic;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Models.Candidates
{
    public class CandidateListingViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public string BirthDate { get; set; }

        public int Interviews { get; set; }

        public ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();

        public Recruiter Recruiter { get; set; }
    }
}
