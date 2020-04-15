namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

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
    }
}
