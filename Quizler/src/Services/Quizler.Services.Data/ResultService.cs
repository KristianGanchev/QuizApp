namespace Quizler.Services.Data
{
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Answers;
    using Quizler.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ResultService : IResultService
    {
        private readonly IDeletableEntityRepository<Result> resultRepository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;
        private readonly IRepository<AnswerResult> answerResult;

        public ResultService(IDeletableEntityRepository<Result> resultRepository, IDeletableEntityRepository<Quiz> quizRepository, IRepository<AnswerResult> answerResult)
        {
            this.resultRepository = resultRepository;
            this.quizRepository = quizRepository;
            this.answerResult = answerResult;
        }

        public async Task<int> CreateAync(int points, int maxPoints, string studentId, int quizId, List<AnswerResponse> answers)
        {
            var result = new Result
            {
                Points = points,
                MaxPoints = maxPoints,
                StudentId = studentId,
                QuizId = quizId
            };

            await this.resultRepository.AddAsync(result);
            await this.resultRepository.SaveChangesAsync();

            foreach (var answerModel in answers)
            {
                var answer = new Answer
                {
                    Id = answerModel.Id,
                    Text = answerModel.Text,
                    IsCorrect = answerModel.IsCorrect
                };

                var selectedAnswer = new AnswerResult
                {
                    AnswerId = answer.Id,
                    ResultId = result.Id
                };

                result.SelectedAnswers.Add(selectedAnswer);
                await this.answerResult.AddAsync(selectedAnswer);
            };

            await this.answerResult.SaveChangesAsync();

            var quiz = await this.quizRepository.GetByIdWithDeletedAsync(quizId);
            quiz.Plays++;
            this.quizRepository.Update(quiz);
            await this.quizRepository.SaveChangesAsync();

            return result.Id;
        }

        public T GetByQuizId<T>(int quizId)
        {
            var result = this.resultRepository.All().Where(r => r.QuizId == quizId).To<T>().FirstOrDefault();

            return result;
        }
    }
}
