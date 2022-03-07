
using MyTested.AspNetCore.Mvc;
using Xunit;
using ZonartUsers.Data.Models;
using System.Linq;
using Zonart.Controllers;
using ZonartUsers.Models.Orders;

namespace ZonartUsers.Tests.Controllers
{
    public class OrdersControllerTest
    {
        [Theory]
        [InlineData(1)]
        public void GetCreateShouldReturnViewWithCorrectModel(int id)
        {
            MyController<OrdersController>
                .Instance()
                .Calling(c => c.Create(id))
                .ShouldReturn()
                .View(new OrderTemplateModel { TemplateId = id});
        }


        [Theory]
        [InlineData(1, "Iva Yorgova", "email@some.bg")]
        public void PostCreateShouldReturnViewWithCorrectModel(int templateId,
           string name, string email)
        {
            MyController<OrdersController>
                .Instance()
                .Calling(c => c.Create(new OrderTemplateModel
                {
                    TemplateId = templateId,
                    FullName = name,
                    Email = email
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                .WithSet<Order>(orders =>
                {
                    orders.Any(o =>
                    o.Name == name &&
                    o.Email == email &&
                    o.TemplateId == templateId);
                }))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Confirm", "Orders");
        }


        [Fact]
        public void ConfirmShouldReturnCorrectView()
        {
            MyController<OrdersController>
                .Instance()
                .Calling(c => c.Confirm())
                .ShouldReturn()
                .View();
        }
    }
}
