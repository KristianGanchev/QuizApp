namespace Quizler.Services.Data
{
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using System.Threading.Tasks;

    public class AnswersServices : IAnswersServices
    {
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IDeletableEntityRepository<Question> questionRepository;

        public AnswersServices(IDeletableEntityRepository<Answer> answerRepository, IDeletableEntityRepository<Question> questionRepository)
        {
            this.answerRepository = answerRepository;
            this.questionRepository = questionRepository;
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

            var question = await this.questionRepository.GetByIdWithDeletedAsync(questionId);
          //  question.Answers.Add(answer);

            return answer.Id;
        }
    }
}
