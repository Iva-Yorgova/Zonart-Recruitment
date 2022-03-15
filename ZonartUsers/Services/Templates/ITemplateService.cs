

using System.Collections.Generic;
using System.Linq;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Templates;

namespace ZonartUsers.Services.Templates
{
    public interface ITemplateService
    {
        bool Edit(
           int templateId,
           string name,
           double price,
           string description,
           string Category,
           string imageUrl);

        void Add(
            string name,
            double price,
            string description,
            string category,
            string imageUrl);

        bool Delete(
            int templateId);

        List<TemplateListingViewModel> GetTemplates();

        List<TemplateListingViewModel> GetTemplatesQuery(IQueryable<Template> templatesQuery, AllTemplatesModel query);

        IQueryable<Template> SortTemplateQuery(IQueryable<Template> templatesQuery, AllTemplatesModel query);

        IQueryable<Template> GetTemplatesBySearchTerm(IQueryable<Template> templatesQuery, AllTemplatesModel query);

        TemplateLayoutModel GetTemplateLayoutById(int id);

        TemplateListingViewModel GetTemplateListingById(int id);
    }
}
