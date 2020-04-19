namespace Quizler.Services.Data
{
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
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

            var quiz = await this.quizRepository.GetByIdWithDeletedAsync(quizId);
            //quiz.Questions.Add(question);

            return question.Id;
        }
    }
}
