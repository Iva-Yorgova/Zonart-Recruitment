using Moq;
using ZonartUsers.Services.Statistics;

namespace ZonartUsers.Tests.Mocks
{
    public class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalUsers = 4,
                        TotalContacts = 7,
                        TotalOrders = 10,
                        TotalTemplates = 6
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
