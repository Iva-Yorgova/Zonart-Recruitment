using System;


namespace ZonartUsers.Models.Interviews
{
    public class InterviewListingViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Date { get; set; }

        public string RecruiterName { get; set; }

        public string CandidateName { get; set; }

        public string JobName { get; set; }

    }
}
