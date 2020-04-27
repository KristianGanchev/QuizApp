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
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public QuizzesService(IDeletableEntityRepository<Quiz> quizRepository, IDeletableEntityRepository<Question> questionRepository, IDeletableEntityRepository<Answer> answerRepository)
        {
            this.quizRepository = quizRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
        }

        public async Task<int> CreateAsync(string name, int categoryId, ApplicationUser user, string imageUrl)
        {

            if (imageUrl == null)
            {
                imageUrl = "/images/default_quiz_image.png";
            }

            var quiz = new Quiz
            {
                Name = name,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                Creator = user
            };

            await this.quizRepository.AddAsync(quiz);
            await this.quizRepository.SaveChangesAsync();

            return quiz.Id;
        }

        public async Task<int> UpdateAsync(string name, int categoryId, string imageUrl, int id)
        {
            var quiz = await this.quizRepository.GetByIdWithDeletedAsync(id);

            quiz.Name = name;
            quiz.CategoryId = categoryId;

            if (imageUrl != null)
            {
                quiz.ImageUrl = imageUrl;
            }

            this.quizRepository.Update(quiz);
            await this.quizRepository.SaveChangesAsync();

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

        public IEnumerable<T> GetByCategory<T>(string categoryName)
        {
            IQueryable<Quiz> query = this.quizRepository.All().Where(q => q.Category.Name == categoryName).OrderByDescending(q => q.CreatedOn).Take(5);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByUser<T>(string userId)
        {
            IQueryable<Quiz> query = this.quizRepository.All().Where(q => q.CreatorId == userId).OrderByDescending(q => q.CreatedOn.Day);

            return query.To<T>().ToList();
        }

        public async Task<int> DeleteAsync(int id) 
        {
            var quiz = await this.quizRepository.GetByIdWithDeletedAsync(id);
            var questions = this.questionRepository.All().Where(q => q.QuizId == id);

            foreach (var question in questions)
            {
                var answers = this.answerRepository.All().Where(a => a.QuestionId == question.Id);

                foreach (var answer in answers)
                {
                    this.answerRepository.Delete(answer);
                }

                this.questionRepository.Delete(question);
            }
            
            this.quizRepository.Delete(quiz);

            await this.answerRepository.SaveChangesAsync();
            await this.questionRepository.SaveChangesAsync();
            var result =  await this.quizRepository.SaveChangesAsync();

            return result;
        }

        public IEnumerable<T> Search<T>(string searchQuery) 
        {
            var quizes = this.quizRepository.All().Where(q => q.Name.Contains(searchQuery)).To<T>();

            return quizes;
        }
    }
}
