using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using ZonartUsers.Models.Users;
using ZonartUsers.Services.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ZonartUsers.Controllers
{
    using static WebConstants;

    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ZonartUsersDbContext data;
        private readonly IMemoryCache cache;
        private readonly IQuestionService service;

        public UsersController(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ZonartUsersDbContext data, 
            IMemoryCache cache,
            IQuestionService service)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.data = data;
            this.cache = cache;
            this.service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var registeredUser = new User
            {
                Email = user.Email,
                UserName = user.Email,
                FullName = user.FullName
            };

            var result = await this.userManager.CreateAsync(registeredUser, user.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var loggedInUser = this.userManager.FindByEmailAsync(user.Email).Result;

            if (loggedInUser ==  null)
            {
                ModelState.AddModelError(string.Empty, InvalidCredentials);
                return View(user);
            }
   
            var passwordIsValid = await this.userManager.CheckPasswordAsync(loggedInUser, user.Password);

            if (!passwordIsValid)
            {
                ModelState.AddModelError(string.Empty, InvalidCredentials);
                return View(user);
            }

            else
            {
                await this.signInManager.SignInAsync(loggedInUser, true);
                return RedirectToAction("Welcome", "Users", loggedInUser.Id);
                //return RedirectToAction("Index", "Home");
            }
        }

        
        [Authorize]
        public async Task<IActionResult> Logout()
        {           
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.userManager.ChangePasswordAsync(user, "", "");

            return View();
        }

        public IActionResult Welcome(int userId)
        {
            return View(userId);
        }

        public IActionResult WelcomeAdmin(int userId)
        {
            return View(userId);
        }

    }
}
