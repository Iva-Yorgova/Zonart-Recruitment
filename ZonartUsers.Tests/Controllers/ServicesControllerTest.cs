using ZonartUsers.Controllers;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace ZonartUsers.Tests.Controllers
{    
    public class ServicesControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
                MyController<ServicesController>
                .Instance()
                .Calling(c => c.All())
                .ShouldReturn()
                .View();
        }
    }
}
