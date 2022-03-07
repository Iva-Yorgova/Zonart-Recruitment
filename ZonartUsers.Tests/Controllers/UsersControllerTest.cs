
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;
using ZonartUsers.Controllers;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Users;
using ZonartUsers.Services.Questions;
using ZonartUsers.Tests.Mocks;

namespace ZonartUsers.Tests.Controllers
{
    public class UsersControllerTest
    {

        [Fact]
        public void RegisterShouldReturnViewWithCorrectModel()
        {
            MyController<UsersController>
                .Instance()
                .Calling(c => c.Register())
                .ShouldReturn()
                .View();
        }


        [Theory]
        [InlineData("mail@some.bg", "User TestName", "123456")]
        public void PostRegisterShouldReturnRedirectWithValidModel(string email, string name, string pass)
            => MyController<UsersController>
                .Instance()
                .Calling(c => c.Register(new RegisterUserModel
                {
                    Email = email,
                    FullName = name,
                    Password = pass
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<User>(users => users
                        .Any(u =>
                            u.FullName == name &&
                            u.Email == email)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<UsersController>(c => c.Login()));


        [Fact]
        public void LoginShouldReturnViewWithCorrectModel()
        {

            MyController<UsersController>
                .Instance()
                .Calling(c => c.Login())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void LogoutShouldReturnRedirect()
        {
            MyController<UsersController>
                .Instance()
                .Calling(c => c.Logout())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
        }

    }


}
