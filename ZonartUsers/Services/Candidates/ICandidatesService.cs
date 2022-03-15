using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Candidates;

namespace ZonartUsers.Services.Candidates
{
    public interface ICandidatesService
    {
        List<CandidateListingViewModel> GetCandidates();

        CandidateListingViewModel GetCandidateListingById(string id);

        EditCandidateFormModel GetCandidateEditById(string id);

        Candidate GetCandidateById(string id);

        bool CheckCandidate(CreateCandidateFormModel model);

        bool ValidateModel(CreateCandidateFormModel model);

        bool ValidateEditModel(EditCandidateFormModel model);

        Recruiter GetRecruiterByName(string name);
    }
}
