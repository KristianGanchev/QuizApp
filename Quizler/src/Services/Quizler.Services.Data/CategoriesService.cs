namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Category> query = this.categoriesRepository.All().OrderBy(c => c.Name);

            return query.To<T>().ToList();
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();

            return category.Id;
        }

        public T GetByName<T>(string name) 
        {
            var category = this.categoriesRepository.All().Where(c => c.Name == name).To<T>().FirstOrDefault();

            return category;
        }
    }
}
