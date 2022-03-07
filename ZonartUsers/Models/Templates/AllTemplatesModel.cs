using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Templates
{
    public class AllTemplatesModel
    {  
        public const int TemplatesPerPage = 3;

        public string Category { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalTemplates { get; set; }

        public TemplateSorting Sorting { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<TemplateListingViewModel> Templates { get; set; }

        public IEnumerable<TemplateListingViewModel> DbTemplates { get; set; }
    }
}
