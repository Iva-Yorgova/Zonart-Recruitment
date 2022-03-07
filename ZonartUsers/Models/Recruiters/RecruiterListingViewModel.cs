﻿
using System.Collections.Generic;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Models.Recruiters
{
    public class RecruiterListingViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Epost { get; set; }

        public string Country { get; set; }

        public int ExperienceLevel { get; set; }

        public int FreeInterviewSlots { get; set; }

        public int Interviews { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
    }
}
