using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data;
using ZonartUsers.Models.Candidates;
using ZonartUsers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using ZonartUsers.Services.Candidates;

namespace ZonartUsers.Services.Candidates
{
    public class CandidateServise : ICandidatesService
    {

        private readonly ZonartUsersDbContext data;

        public CandidateServise(ZonartUsersDbContext data)
        {
            this.data = data;
        }

        public List<CandidateListingViewModel> GetCandidates()
        {
            var candidates = this.data
                .Candidates
                .OrderBy(c => c.FirstName)
                .Select(c => new CandidateListingViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    BirthDate = c.BirthDate,
                    CandidateSkills = c.CandidateSkills,
                    Recruiter = c.Recruiter,
                    Interviews = this.data.Interviews.Where(i => i.CandidateName == c.FirstName + ' ' + c.LastName).Count()
                })
                .ToList();

            return candidates;
        }

        public bool CheckCandidate(CreateCandidateFormModel model)
        {
            bool candidateExist = this.data.Candidates
                .Any(c => c.FirstName == model.FirstName &&
                c.LastName == model.LastName);

            return candidateExist;
           
        }

        public bool ValidateModel(CreateCandidateFormModel model)
        {
            bool invalidModel = string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Bio) ||
                string.IsNullOrEmpty(model.BirthDate) ||
                string.IsNullOrEmpty(model.Skill) ||
                string.IsNullOrEmpty(model.RecruiterName) ||
                string.IsNullOrEmpty(model.RecruiterEmail) ||
                string.IsNullOrEmpty(model.RecruiterCountry);

            return invalidModel;
        }

        public Recruiter GetRecruiterByName(string name)
        {
            var recruiter = this.data.Recruiters.FirstOrDefault(r => r.Name == name);
            return recruiter;
        }

        public CandidateListingViewModel GetCandidateListingById(string id)
        {
            var candidate = this.data
                .Candidates
                .Where(c => c.Id == id)
                .Select(c => new CandidateListingViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    BirthDate = c.BirthDate,
                    CandidateSkills = c.CandidateSkills,
                    Bio = c.Bio,
                    Interviews = this.data.Interviews.Where(i => i.CandidateName == c.FirstName + ' ' + c.LastName).Count()
                })
                .FirstOrDefault();

            return candidate;
        }

        public EditCandidateFormModel GetCandidateEditById(string id)
        {
            var candidate = this.data.Candidates
                .Where(c => c.Id == id)
                .Select(c => new EditCandidateFormModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    BirthDate = c.BirthDate,
                    Bio = c.Bio,
                    CandidateSkills = c.CandidateSkills
                })
                .FirstOrDefault();

            return candidate;
        }

        public Candidate GetCandidateById(string id)
        {
            var candidate = this.data.Candidates.FirstOrDefault(t => t.Id == id);
            return candidate;
        }

        public bool ValidateEditModel(EditCandidateFormModel model)
        {
            bool invalidEditModel = string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Bio) ||
                string.IsNullOrEmpty(model.BirthDate);

            return invalidEditModel;
        }
    }
}
