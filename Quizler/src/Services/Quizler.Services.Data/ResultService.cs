namespace Quizler.Services.Data
{
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ResultService : IResultService
    {
        private readonly IDeletableEntityRepository<Result> resultRepository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;

        public ResultService(IDeletableEntityRepository<Result> resultRepository, IDeletableEntityRepository<Quiz> quizRepository)
        {
            this.resultRepository = resultRepository;
            this.quizRepository = quizRepository;
        }

        public async Task<int> CreateAync(int points, int maxPoints, string studentId, int quizId)
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
