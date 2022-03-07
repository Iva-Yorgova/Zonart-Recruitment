using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Models.Services;
using ZonartUsers.Services.Statistics;

namespace ZonartUsers.Controllers
{
    public class AboutController : Controller
    {
        private readonly IStatisticsService statistics;

        public AboutController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        public IActionResult OurStory()
        {
            var totalStatistics = this.statistics.Total();

            return View(new ServicesViewModel
            {
                TotalTemplates = totalStatistics.TotalTemplates,
                TotalUsers = totalStatistics.TotalUsers,
                TotalOrders = totalStatistics.TotalOrders,
                TotalContacts = totalStatistics.TotalContacts
            });
        }

    }
}
