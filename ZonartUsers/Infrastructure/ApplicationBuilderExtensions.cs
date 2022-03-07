using System;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ZonartUsers.Infrastructure
{
    using static WebConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<ZonartUsersDbContext>();

            data.Database.Migrate();

            SeedTemplates(data);
            SeedQuestions(data);
            SeedAdministrator(serviceProvider);

            return app;
        }

        private static void SeedTemplates(ZonartUsersDbContext data)
        {
            if (data.Templates.Any())
            {
                return;
            }

            data.Templates.AddRange(new[]
            {
            new Template { Name = Templates.FirstTemplateName, Price = Templates.TemplateFirstPrice, ImageUrl = Templates.TemplateFirstImg, Description = Templates.TemplateFirstDescription, Category = "Web" },
            new Template { Name = Templates.SecondTemplateName, Price = Templates.TemplateSecondPrice, ImageUrl = Templates.TemplateSecondImg, Description = Templates.TemplateSecondDescription, Category = "Web" },
            new Template { Name = Templates.ThirdTemplateName, Price = Templates.TemplateThirdPrice, ImageUrl = Templates.TemplateThirdImg, Description = Templates.TemplateThirdDescription, Category = "Web" },
            new Template { Name = Templates.FourthTemplateName, Price = Templates.TemplateFourthPrice, ImageUrl = Templates.TemplateFourthImg, Description = Templates.TemplateFourthDescription, Category = "Web" },
            new Template { Name = Templates.FifthTemplateName, Price = Templates.TemplateFifthPrice, ImageUrl = Templates.TemplateFifthImg, Description = Templates.TemplateFifthDescription, Category = "Print" },
            new Template { Name = Templates.SixthTemplateName, Price = Templates.TemplateSixthPrice, ImageUrl = Templates.TemplateSixthImg, Description = Templates.TemplateSixthDescription, Category = "Print" },
            new Template { Name = Templates.SeventhTemplateName, Price = Templates.TemplateSeventhPrice, ImageUrl = Templates.TemplateSeventhImg, Description = Templates.TemplateSeventhDescription, Category = "Print" }
             });

            data.SaveChanges();
        }

        private static void SeedQuestions(ZonartUsersDbContext data)
        {
            if (data.Questions.Any())
            {
                return;
            }

            data.Questions.AddRange(new[]
            {
            new Question { Text = Questions.FirstQuestionText, Answer = Questions.FirstQuestionAnswer },
            new Question { Text = Questions.SecondQuestionText, Answer = Questions.SecondQuestionAnswer },
            new Question { Text = Questions.ThirdQuestionText, Answer = Questions.ThirdQuestionAnswer },
            new Question { Text = Questions.FourthQuestionText, Answer = Questions.FourthQuestionAnswer },
            new Question { Text = Questions.FifthrstQuestionText, Answer = Questions.FifthrstQuestionAnswer },
            new Question { Text = Questions.SixthQuestionText, Answer = Questions.SixthQuestionAnswer },
            new Question { Text = Questions.SeventhQuestionText, Answer = Questions.SeventhQuestionAnswer },
            });

            data.SaveChanges();
        }

        

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () => {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdminRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = AdminEmail;
                const string adminPassword = AdminPassword;

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = AdminFullName
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();           
        }
    }
}


