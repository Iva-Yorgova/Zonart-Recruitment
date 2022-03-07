using MyTested.AspNetCore.Mvc;
using Xunit;
using ZonartUsers.Controllers;

namespace ZonartUsers.Tests.Controllers
{
    public class ProjectsControllerTest
    {

        [Fact]
        public void AllShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.All())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void BrochuresShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Brochures())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void BooksShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Books())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void KidsShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Kids())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void LogosShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Logos())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void PostersShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Posters())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void StationaryShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Stationary())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void VarietyShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Variety())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WebShouldReturnCorrectView()
        {
            MyController<ProjectsController>
                .Instance()
                .Calling(c => c.Web())
                .ShouldReturn()
                .View();
        }
    }
}
