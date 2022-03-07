using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Models.Services;
using ZonartUsers.Services.Statistics;

namespace ZonartUsers.Controllers
{
    public class ServicesController : Controller
    {

        private readonly IStatisticsService statistics;

        public ServicesController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        public IActionResult All()
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
