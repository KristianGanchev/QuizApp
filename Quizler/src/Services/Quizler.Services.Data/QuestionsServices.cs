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
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public QuestionsServices(IDeletableEntityRepository<Question> questionRepository, IDeletableEntityRepository<Answer> answerRepository) 
        {
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
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

        public async Task<int> DeleteAsync(int id)
        {
            var question = await this.questionRepository.GetByIdWithDeletedAsync(id);
            var answers = this.answerRepository.All().Where(a => a.QuestionId == id);

            foreach (var asnwer in answers)
            {
                this.answerRepository.Delete(asnwer);
            }

            this.questionRepository.Delete(question);

            await this.answerRepository.SaveChangesAsync();
            var result = await this.questionRepository.SaveChangesAsync();

            return result;
        }

        public T GetByQuizId<T>(int quizId) 
        {
            var question = this.questionRepository.All().Where(q => q.QuizId == quizId).To<T>().FirstOrDefault();

            return question;
        }

        public int GetId(string questionName, int quizId) 
        {
            var question = this.questionRepository.All().Where(q => q.Text == questionName && q.QuizId == quizId).FirstOrDefault();

            return question.Id;
        }

        public IEnumerable<T> GetAll<T>(int quizId) 
        {
            IQueryable<Question> query = this.questionRepository.All().Where(q => q.QuizId == quizId).OrderBy(q => q.CreatedOn);

            return query.To<T>().ToList();
        }
    }
}
