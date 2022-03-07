using System.Collections.Generic;

namespace ZonartUsers.Models.Recruiters
{
    public class AllRecruitersViewModel
    {
        public int Level { get; set; }
        public IEnumerable<RecruiterListingViewModel> Recruiters { get; set; }

    }
}
