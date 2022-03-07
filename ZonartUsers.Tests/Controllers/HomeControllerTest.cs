using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Controllers;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ZonartUsers.Tests.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var homeController = new HomeController();
            //var data = DatabaseMock.Instance;

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            
        }


        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController();

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void PipelineTest()
        {
            MyMvc.Pipeline()
                 .ShouldMap("/Home/Error")
                 .To<HomeController>(c => c.Error())
                 .Which()
                 .ShouldReturn()
                 .View();
          
        }
    }
}
