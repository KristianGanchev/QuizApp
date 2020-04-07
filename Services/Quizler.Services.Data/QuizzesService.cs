namespace Quizler.Services.Data
{
    using System.Threading.Tasks;

    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;

    public class QuizzesService : IQuizzesService
    {
        private readonly Quizler.Data.Common.Repositories.IDeletableEntityRepository<Quiz> quizRepository;

        public QuizzesService(IDeletableEntityRepository<Quiz> quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        public async Task<int> CreateAsync(string name, int categoryId, string userId)
        {
            var imageUrl = "/images/default_quiz_image.png";

            var quiz = new Quiz
            {
                Name = name,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                CreatorId = userId,
            };

            await this.quizRepository.AddAsync(quiz);
            await this.quizRepository.SaveChangesAsync();

            return quiz.Id;
        }
    }
}
