namespace Quizler.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Quizler.Common;
    using Quizler.Data.Models;
    using System;
    using System.Threading.Tasks;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@Quizler.com",
                };

                var password = "123456";

                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
