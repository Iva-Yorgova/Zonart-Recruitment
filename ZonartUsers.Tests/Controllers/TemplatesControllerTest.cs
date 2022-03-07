using Microsoft.AspNetCore.Mvc;
using ZonartUsers.Controllers;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using Xunit;
using ZonartUsers.Data;
using ZonartUsers.Models.Contacts;
using ZonartUsers.Data.Models;
using System.Linq;
using ZonartUsers.Tests.Mocks;
using ZonartUsers.Models.Templates;
using System.Collections.Generic;
using ZonartUsers.Services.Templates;
using Microsoft.AspNetCore.Authorization;

namespace ZonartUsers.Tests.Controllers
{
    public class TemplatesControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            MyController<TemplatesController>
                .Instance()
                .Calling(c => c.All())
                .ShouldReturn()
                .View();
        }


        [Fact]
        public void AllShouldReturnViewWithCorrectModel2()
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(new List<TemplateListingViewModel>());
            var service = new TemplateService(data);

            data.Templates.Add(new Template { Name = "test", Price = 100, Description = "some" });
            data.Templates.Add(new Template { Name = "test", Price = 50, Description = "some" });
            data.SaveChanges();
        
            var controller = new TemplatesController(data, cache, service);
        
            // Act
            var result = controller.All();
        
            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
        
            var model = viewResult.Model;
        
            var viewModel = Assert.IsType<List<TemplateListingViewModel>>(model);
        
            Assert.Equal(2, data.Templates.Count());
        
        }


        [Theory]
        [InlineData(1)]
        public void DetailsShouldReturnViewWithCorrectModel(int id)
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new TemplateService(data);

            data.Templates.Add(new Template { Name = "test", Price = 100 });
            data.SaveChanges();

            var controller = new TemplatesController(data, cache, service);

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var viewModel = Assert.IsType<TemplateLayoutModel>(model);

            Assert.Equal(1, data.Templates.Count());

        }


        [Theory]
        [InlineData(1)]
        public void EditShouldReturnViewWithCorrectModel(int id)
        {
            // Arrange
            using var data = DatabaseMock.Instance;
            using var cache = MemoryCacheMock.GetMemoryCache(null);
            var service = new TemplateService(data);

            data.Templates.Add(new Template { Name = "test", Price = 100 });
            data.SaveChanges();

            var controller = new TemplatesController(data, cache, service);

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<TemplateListingViewModel>(model);
        }


        [Theory]
        [InlineData(1, "Name", "Description", 40, "url")]
        public void PostEditShouldReturnRedirectToActionAndEditTemplate(int id, string name, string description, double price, string url)
        {
            MyController<TemplatesController>
                .Instance()
                .Calling(c => c.Edit(new TemplateListingViewModel
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    Price = price,
                    ImageUrl = url
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
                .Data(data => data
                .WithSet<Template>(temp =>
                {
                    temp.Any(t =>
                    t.Name == name &&
                    t.Description == description &&
                    t.ImageUrl == url &&
                    t.Price == price);
                }))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();
                //.RedirectToAction("All", "Templates");
        }

        [Theory]
        [InlineData(1)]
        public void DeleteShouldReturnRedirect(int id)
        {
            MyController<TemplatesController>
                .Instance()
                .Calling(c => c.Delete(id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();
            //.RedirectToAction("All", "Questions");

        }

        [Theory]
        [InlineData("Name", "Description", 40, "url")]
        public void AddShouldReturnRedirect(string name, string description, double price, string url)
        {
            MyController<TemplatesController>
                .Instance()
                .Calling(c => c.Add(new AddTemplateModel
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    ImageUrl = url
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();
            //.RedirectToAction("All", "Questions");

        }

        [Fact]
        public void AddShouldReturnViewWithCorrectModel()
        {
            MyController<TemplatesController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(new AddTemplateModel());
        }
    }
}
