
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Infrastructure;
using ZonartUsers.Models.Candidates;
using Microsoft.AspNetCore.Authorization;

namespace ZonartUsers.Controllers
{
    using static WebConstants;

    public class CandidatesController : Controller
    {
        private readonly ZonartUsersDbContext data;

        public CandidatesController(ZonartUsersDbContext data) 
            => this.data = data;

        public IActionResult All()
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

            return View(candidates);
        }

        [Authorize]
        public IActionResult Create()
        {
            if (!User.IsAdmin())
            {
                ModelState.AddModelError(string.Empty, "Invalid Credentials!");
                return View();
            }
            return View(new CreateCandidateFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateCandidateFormModel model)
        {
                      
            if (this.data.Candidates
                .Any(c => c.FirstName == model.FirstName && 
                c.LastName == model.LastName))
            {
                ModelState.AddModelError(string.Empty, "Candidate already exists!");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.FirstName) || 
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Bio) ||
                string.IsNullOrEmpty(model.BirthDate) ||
                string.IsNullOrEmpty(model.Skill) ||
                string.IsNullOrEmpty(model.RecruiterName) ||
                string.IsNullOrEmpty(model.RecruiterEmail) ||
                string.IsNullOrEmpty(model.RecruiterCountry))
            {
                return View(model);
            }

            var recruiter = this.data.Recruiters.FirstOrDefault(r => r.Name == model.RecruiterName);

            if (recruiter == null)
            {
                recruiter = new Recruiter
                {
                    Name = model.RecruiterName,
                    Epost = model.RecruiterEmail,
                    Country = model.RecruiterCountry
                };
                this.data.Recruiters.Add(recruiter);
            }
            else
            {              
                recruiter.ExperienceLevel++;    
            }

            var candidate = new Candidate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                BirthDate = model.BirthDate,
                Bio = model.Bio,
                RecruiterId = recruiter.Id
            };

            if (!this.data.Skills.Any(s => s.Name == model.Skill))
            {
                var skill = new CandidateSkill { Name = model.Skill };
                candidate.CandidateSkills.Add(skill);
                this.data.Skills.Add(new Skill { Name = model.Skill });
            }
            else
            {
                candidate.CandidateSkills.Add(new CandidateSkill{ Name = model.Skill });
            }
            
            // TODO: Check if there is a job with skills that candidate have


            recruiter.Candidates.Add(candidate);

            this.data.Candidates.Add(candidate);          

            this.data.SaveChanges();

            return Redirect("/Candidates/All");
        }

        public IActionResult Details(string id)
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

            return View(candidate);
        }


        [Authorize]
        public IActionResult Edit(string id)
        {
            if (!User.IsAdmin())
            {
                ModelState.AddModelError(string.Empty, "Invalid Credentials!");
                return View();
            }

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

            return View(candidate);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditCandidateFormModel model)
        {

            var candidateData = this.data.Candidates
                .FirstOrDefault(t => t.Id == model.Id);

            if (candidateData == null)
            {
                ModelState.AddModelError(string.Empty, "Candidate not found.");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Bio) ||
                string.IsNullOrEmpty(model.BirthDate))
            {
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            candidateData.FirstName = model.FirstName;
            candidateData.LastName = model.LastName;
            candidateData.Bio = model.Bio;
            candidateData.BirthDate = model.BirthDate;
            candidateData.Email = model.Email;

            if (model.Skill != null)
            {
                candidateData.CandidateSkills.Add(new CandidateSkill { Name = model.Skill });
            }

            this.data.SaveChanges();

            return Redirect("/Candidates/All");
        }

        public IActionResult Delete(string id)
        {
            var candidate = this.data.Candidates
                .Where(c => c.Id == id)
                .FirstOrDefault();

            var candidateSkills = this.data.CandidatesSkills
                .Where(s => s.CandidateId == id)
                .ToList();

            foreach (var skill in candidateSkills)
            {
                this.data.CandidatesSkills.Remove(skill);
            }

            this.data.Candidates.Remove(candidate);
            this.data.SaveChanges();

            return Redirect("/Candidates/All");
        }

    }
}
