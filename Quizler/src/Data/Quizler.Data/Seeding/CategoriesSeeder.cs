namespace Quizler.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Quizler.Data.Models;

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
                new Category { Name = "Mathematics" },
                new Category { Name = "English" },
                new Category { Name = "Computers" },
                new Category { Name = "Science" },
                new Category { Name = "Fun" },
                new Category { Name = "History" },
                new Category { Name = "Other" },
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
