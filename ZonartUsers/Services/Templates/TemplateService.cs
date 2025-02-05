﻿
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Templates;

namespace ZonartUsers.Services.Templates
{
    public class TemplateService : ITemplateService
    {
        private readonly ZonartUsersDbContext data;

        public TemplateService(ZonartUsersDbContext data)
        {
            this.data = data;
        }

        public bool Edit(
           int templateId,
           string name,
           double price,
           string description,
           string category,
           string imageUrl)
        {
            var templateData = this.data.Templates
                .FirstOrDefault(t => t.Id == templateId);

            if (templateData == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(name)  || price <= 0 || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(imageUrl))
            {
                return false;
            }

            templateData.Name = name;
            templateData.Price = price;
            templateData.Description = description;
            templateData.ImageUrl = imageUrl;
            templateData.Category = category;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int templateId)
        {
            var templateData = this.data.Templates
                .FirstOrDefault(t => t.Id == templateId);

            if (templateData == null)
            {
                return false;
            }

            this.data.Templates.Remove(templateData);
            this.data.SaveChanges();

            return true;
        }

        public void Add(string name, double price, string description, string imageUrl, string category)
        {
            var newTemplate = new Template
            {
                Name = name,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                Category = category
            };

            this.data.Templates.Add(newTemplate);
            this.data.SaveChanges();
        }

        public List<TemplateListingViewModel> GetTemplates()
        {
            List<TemplateListingViewModel> dbTemplates = new List<TemplateListingViewModel>();

            using (MySqlConnection connection = new MySqlConnection("server=pepe.rdb.superhosting.bg;port=3306;user=yorgovan_ivayorgova;password=noahnoah7777;database=yorgovan_yorgovaDB"))
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Templates", connection);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TemplateListingViewModel dbTemplate = new TemplateListingViewModel();

                    dbTemplate.Id = int.Parse(reader["Id"].ToString());
                    dbTemplate.Name = reader["Name"].ToString();
                    dbTemplate.Category = reader["Category"].ToString();
                    dbTemplate.Description = reader["Description"].ToString();
                    dbTemplate.Price = int.Parse(reader["Price"].ToString());

                    dbTemplates.Add(dbTemplate);
                }
                reader.Close();
            }
            
            return dbTemplates;
        }

        public List<TemplateListingViewModel> GetTemplatesQuery(IQueryable<Template> templatesQuery, AllTemplatesModel query)
        {
            var templates = templatesQuery
                .Skip((query.CurrentPage - 1) * AllTemplatesModel.TemplatesPerPage)
                .Take(AllTemplatesModel.TemplatesPerPage)
                .Select(t => new TemplateListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl,
                    Price = t.Price,
                    Description = t.Description,
                    Category = t.Category
                })
                .ToList();

            return templates;
        }

        public IQueryable<Template> SortTemplateQuery(IQueryable<Template> templatesQuery, AllTemplatesModel query)
        {
            templatesQuery = query.Sorting switch
            {
                TemplateSorting.Price => templatesQuery.OrderByDescending(t => t.Price),
                TemplateSorting.Category => templatesQuery.OrderBy(t => t.Category),
                TemplateSorting.DateCreated or _ => templatesQuery.OrderByDescending(t => t.Id)
            };

            return templatesQuery;
        }

        public IQueryable<Template> GetTemplatesBySearchTerm(IQueryable<Template> templatesQuery, AllTemplatesModel query)
        {
            templatesQuery = templatesQuery
                    .Where(t => t.Description.ToLower().Contains(query.SearchTerm.ToLower())
                    || t.Name.ToLower().Contains(query.SearchTerm.ToLower()));

            return templatesQuery;
        }

        public TemplateLayoutModel GetTemplateLayoutById(int id)
        {
            var template = this.data.Templates
                .Where(t => t.Id == id)
                .Select(t => new TemplateLayoutModel
                {
                    Id = id,
                    Name = t.Name,
                    Description = t.Description,
                    Category = t.Category,
                    Price = t.Price
                })
                .FirstOrDefault();

            return template;
        }

        public TemplateListingViewModel GetTemplateListingById(int id)
        {
            var template = this.data.Templates
                .Where(t => t.Id == id)
                .Select(t => new TemplateListingViewModel
                {
                    Name = t.Name,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl,
                    Description = t.Description,
                    Category = t.Category,
                    Id = id
                })
                .FirstOrDefault();

            return template;
        }
    }
}
