namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class QuizzesService : IQuizzesService
    {
        private readonly IDeletableEntityRepository<Quiz> quizRepository;

        public QuizzesService(IDeletableEntityRepository<Quiz> quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        public async Task<int> CreateAsync(string name, int categoryId, ApplicationUser user)
        {
            var imageUrl = "/images/default_quiz_image.png";

            var quiz = new Quiz
            {
                Name = name,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                CreatorId = user.Id
            };

            await this.quizRepository.AddAsync(quiz);
            await this.quizRepository.SaveChangesAsync();

          //  user.Quizzes.Add(quiz);

            return quiz.Id;
        }

        public T GetById<T>(int id) 
        {
            var quiz = this.quizRepository.All().Where(q => q.Id == id).To<T>().FirstOrDefault();

            return quiz;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Quiz> query = this.quizRepository.All().OrderBy(q => q.CreatedOn);

            return query.To<T>().ToList();
        }
    }
}
