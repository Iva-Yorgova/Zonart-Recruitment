﻿using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Infrastructure;
using ZonartUsers.Models.Templates;
using ZonartUsers.Services.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ZonartUsers.Controllers
{
    using static WebConstants.Cache;
    using static WebConstants;
    public class TemplatesController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly ITemplateService service;

        public TemplatesController(ZonartUsersDbContext data, ITemplateService service)
        {
            this.data = data;
            this.service = service;
        }

        public IActionResult All([FromQuery]AllTemplatesModel query)
        {
            var templatesQuery = this.data.Templates.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                templatesQuery = templatesQuery
                    .Where(t => t.Category == query.Category);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                templatesQuery = this.service.GetTemplatesBySearchTerm(templatesQuery, query);
            }

            var categories = this.data.Templates
                .Select(t => t.Category)
                .Distinct()
                .ToList();

            templatesQuery = this.service.SortTemplateQuery(templatesQuery, query);

            var totalTemplates = templatesQuery.Count();

            var templates = this.service.GetTemplatesQuery(templatesQuery, query);

            query.Categories = categories;
            query.Templates = templates;
            query.TotalTemplates = totalTemplates;     
            query.DbTemplates = this.service.GetTemplates();

            return View(query);
        }


        public IActionResult Details(int id)
        {
            var template = this.service.GetTemplateLayoutById(id);
            return View(template); 
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var template = this.service.GetTemplateListingById(id);
            return View(template);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(TemplateListingViewModel template)
        {       

            var edited = this.service.Edit(
                template.Id,
                template.Name,
                template.Price,
                template.Description,
                template.Category,
                template.ImageUrl);

            if (!edited)
            {
                return View(template);
            }

            if (!ModelState.IsValid)
            {
                return View(template);
            }

            TempData[GlobalMessageKey] = TemplateEdited;

            return RedirectToAction("All", "Templates");
        }


        [Authorize]
        public IActionResult Add()
        {      
            return View(new AddTemplateModel());
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTemplateModel template)
        {
            if (!User.IsAdmin())
            {
                return BadRequest(InvalidCredentials);
            }

            if (!ModelState.IsValid)
            {
                return View(template);
            }

            this.service.Add(template.Name, template.Price, template.Description, template.ImageUrl, template.Category);

            TempData[GlobalMessageKey] = TemplateAdded;

            return RedirectToAction("All", "Templates");
        }


        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!User.IsAdmin())
            {
                return BadRequest(InvalidCredentials);
            }

            var deleted = this.service.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = TemplateDeleted;

            return RedirectToAction("All", "Templates");
        }

    }
}
