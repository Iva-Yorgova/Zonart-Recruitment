
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Infrastructure;
using ZonartUsers.Models.Candidates;
using Microsoft.AspNetCore.Authorization;
using ZonartUsers.Services.Candidates;

namespace ZonartUsers.Controllers
{
    using static WebConstants;

    public class CandidatesController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly ICandidatesService service;

        public CandidatesController(ZonartUsersDbContext data, ICandidatesService service)
        {
            this.data = data;
            this.service = service;
        }           

        public IActionResult All()
        {
            var candidates = this.service.GetCandidates();
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
                      
            if (this.service.CheckCandidate(model))
            {
                ModelState.AddModelError(string.Empty, "Candidate already exists!");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (this.service.ValidateModel(model))
            {
                return View(model);
            }

            var recruiter = this.service.GetRecruiterByName(model.RecruiterName); 

            if (recruiter == null)
            {
                recruiter = this.service.CreateRecruiter(model);
                this.data.Recruiters.Add(recruiter);
            }
            else
            {              
                recruiter.ExperienceLevel++;    
            }

            var candidate = this.service.CreateCandidate(model, recruiter);

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
            var candidate = this.service.GetCandidateListingById(id);
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

            var candidate = this.service.GetCandidateEditById(id);
            return View(candidate);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditCandidateFormModel model)
        {
            var candidateData = this.service.GetCandidateById(model.Id);

            if (candidateData == null)
            {
                ModelState.AddModelError(string.Empty, "Candidate not found.");
                return View(model);
            }

            if (this.service.ValidateEditModel(model))
            {
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.service.UpdateCandidateData(candidateData, model);

            if (model.Skill != null)
            {
                candidateData.CandidateSkills.Add(new CandidateSkill { Name = model.Skill });
            }

            this.data.SaveChanges();

            return Redirect("/Candidates/All");
        }

        public IActionResult Delete(string id)
        {
            var candidate = this.service.GetCandidateById(id);

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
