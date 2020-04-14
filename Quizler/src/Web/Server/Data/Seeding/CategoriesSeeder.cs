namespace Quizler.Server.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Quizler.Server.Data;
    using Quizler.Server.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<Category>
            {
                new Category { Name = "Mathemarics" },
                new Category { Name = "English" },
                new Category { Name = "Computers" },
                new Category { Name = "Science" },
                new Category { Name = "Fun" },
                new Category { Name = "History" },
                new Category { Name = "Other" },
            };

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
