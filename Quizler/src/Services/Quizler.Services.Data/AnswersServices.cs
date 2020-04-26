namespace Quizler.Services.Data
{
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AnswersServices : IAnswersServices
    {
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public AnswersServices(IDeletableEntityRepository<Answer> answerRepository)
        {
            this.answerRepository = answerRepository;
        }

     

        public async Task<int> CreateAync(string name, bool isCorrect, int questionId)
        {
            var answer = new Answer
            {
                Text = name,
                IsCorrect = isCorrect,
                QuestionId = questionId,
            };

            await this.answerRepository.AddAsync(answer);
            await this.answerRepository.SaveChangesAsync();

            return answer.Id;
        }

        public IEnumerable<T> GetAll<T>(int id)
        {
            var query = this.answerRepository.All().Where(a => a.QuestionId == id);

            return query.To<T>().ToList();
        }

        public async Task<int> UpdateAsync(string text, bool isCorrect, int id)
        {
            var answer = await this.answerRepository.GetByIdWithDeletedAsync(id);

            answer.Text = text;
            answer.IsCorrect = isCorrect;

            this.answerRepository.Update(answer);
            await this.answerRepository.SaveChangesAsync();

            return answer.Id;
        }

        public T GetByQuestionId<T>(int quizId)
        {
            var answer = this.answerRepository.All().Where(a => a.QuestionId == quizId).To<T>().FirstOrDefault();

            return answer;
        }
    }
}
