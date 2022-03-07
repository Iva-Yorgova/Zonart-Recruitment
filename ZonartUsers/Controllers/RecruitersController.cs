﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Models.Recruiters;

namespace ZonartUsers.Controllers
{
    public class RecruitersController : Controller
    {
        private readonly ZonartUsersDbContext data;

        public RecruitersController(ZonartUsersDbContext data)
            => this.data = data;

        public IActionResult All(string level)
        {
            var recruitersQuery = this.data.Recruiters.AsQueryable();

            int levelNum = 0;

            if (!string.IsNullOrWhiteSpace(level))
            {
                levelNum = int.Parse(level);
                if (levelNum > 0)
                {
                    recruitersQuery = recruitersQuery.Where(r => r.ExperienceLevel == levelNum);
                }
            }

            var recruitersList = recruitersQuery
                .OrderBy(r => r.Name)
                .Select(r => new RecruiterListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Epost = r.Epost,
                    Country = r.Country,
                    ExperienceLevel = r.ExperienceLevel,
                    Candidates = r.Candidates,
                    Interviews = this.data.Interviews.Where(i => i.RecruiterName == r.Name).Count(),
                    FreeInterviewSlots = r.FreeInterviewSlots
                })
                .ToList();

            var recruiters = new AllRecruitersViewModel
            {
                Level = levelNum,
                Recruiters = recruitersList
            };

            return View(recruiters);
        }

        public IActionResult Details(string id)
        {
            var recruiter = this.data
                .Recruiters
                .Where(r => r.Id == id)
                .Select(r => new RecruiterListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Epost = r.Epost,
                    Country = r.Country,
                    ExperienceLevel = r.ExperienceLevel,
                    FreeInterviewSlots = r.FreeInterviewSlots,
                    Candidates = r.Candidates,
                    Interviews = this.data.Interviews.Where(i => i.RecruiterName == r.Name).Count()
                })
                .FirstOrDefault();

            return View(recruiter);
        }
    }
}
