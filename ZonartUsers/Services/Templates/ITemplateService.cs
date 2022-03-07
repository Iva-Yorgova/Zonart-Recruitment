

using System.Collections.Generic;
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
    }
}
