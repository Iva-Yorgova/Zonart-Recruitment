using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Orders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ZonartUsers.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ZonartUsersDbContext data;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public OrdersController(ZonartUsersDbContext data, 
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Create([FromQuery] int templateId)
        {
            var templateName = this.data.Templates
                .Where(t => t.Id == templateId)
                .Select(t => t.Name)
                .FirstOrDefault();

            return View(new OrderTemplateModel
            {
                TemplateId = templateId,
                TemplateName = templateName
            });
        }


        [HttpPost]
        public IActionResult Create(OrderTemplateModel info)
        {

            if (!ModelState.IsValid)
            {
                return View(info);
            }

            var order = new Order
            {
                TemplateId = info.TemplateId,
                Name = info.FullName,
                Email = info.Email,
            };

            this.data.Orders.Add(order);
            this.data.SaveChanges();

            return RedirectToAction("Confirm", "Orders");
        }


        public IActionResult Confirm()
        {
            return View();
        }

    }
}
