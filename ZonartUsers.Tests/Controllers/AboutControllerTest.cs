using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Controllers;
using Xunit;
using ZonartUsers.Models.Services;
using ZonartUsers.Services.Statistics;
using ZonartUsers.Tests.Mocks;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Tests.Controller
{
    public class AboutControllerTest
    {

        [Fact]
        public void OurStoryShouldReturnViewWithCorrectModel()
        {
            // Arrange
            using var data = DatabaseMock.Instance;

            data.Contacts.Add(new Contact { Email = "test", Message = "test", Name = "test" });
            data.Templates.Add(new Template { Name = "test", Price = 100 });
            data.Templates.Add(new Template { Name = "test", Price = 50 });
            data.Orders.Add(new Order { Name = "test" });
            data.Users.Add(new User { Email = "test", FullName = "test" });
            data.SaveChanges();

            var statsServ = new StatisticsService(data);

            var controller = new ServicesController(statsServ);

            var stats = statsServ.Total();

            // Act
            var result = controller.All();

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var viewModel = Assert.IsType<ServicesViewModel>(model);

            Assert.Equal(2, viewModel.TotalTemplates);

        }
    }
}
