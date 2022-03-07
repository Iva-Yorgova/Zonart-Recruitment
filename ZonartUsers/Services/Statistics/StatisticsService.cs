using System.Linq;
using ZonartUsers.Data;

namespace ZonartUsers.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ZonartUsersDbContext data;

        public StatisticsService(ZonartUsersDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalTemplates = this.data.Templates.Count();
            var totalUsers = this.data.Users.Count();
            var totalOrders = this.data.Orders.Count();
            var totalContacts = this.data.Contacts.Count();

            return new StatisticsServiceModel
            {
                TotalTemplates = totalTemplates,
                TotalUsers = totalUsers,
                TotalOrders = totalOrders,
                TotalContacts = totalContacts
            };
        }
    }
}
