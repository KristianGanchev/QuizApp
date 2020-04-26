namespace Quizler.Services.Data
{
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class QuestionsServices : IQuestionsServices
    {
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;

        public QuestionsServices(IDeletableEntityRepository<Question> questionRepository, IDeletableEntityRepository<Quiz> quizRepository) 
        {
            this.questionRepository = questionRepository;
            this.quizRepository = quizRepository;
        }

        public async Task<int> CreateAsync(string name, int points, int quizId)
        {

            var question = new Question
            {
                Text = name,
                Points = points,
                QuizId = quizId,
            };


            await this.questionRepository.AddAsync(question);
            await this.questionRepository.SaveChangesAsync();

            //var quiz = await this.quizRepository.GetByIdWithDeletedAsync(quizId);
            //quiz.Questions.Add(question);

            return question.Id;
        }

        public async Task<int> UpdateAsync(string text, int points, int id)
        {
            var question = await this.questionRepository.GetByIdWithDeletedAsync(id);

            question.Text = text;
            question.Points = points;

            this.questionRepository.Update(question);
            await this.questionRepository.SaveChangesAsync();

            return question.Id;
        }

        public T GetByQuizId<T>(int quizId) 
        {
            var question = this.questionRepository.All().Where(q => q.QuizId == quizId).To<T>().FirstOrDefault();

            return question;
        }

        public IEnumerable<T> GetAll<T>(int quizId) 
        {
            IQueryable<Question> query = this.questionRepository.All().Where(q => q.QuizId == quizId).OrderBy(q => q.CreatedOn);

            return query.To<T>().ToList();
        }
    }
}
