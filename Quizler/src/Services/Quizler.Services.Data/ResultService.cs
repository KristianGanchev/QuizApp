namespace Quizler.Services.Data
{
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using System;
    using System.Threading.Tasks;

    public class ResultService : IResultService
    {
        private readonly IDeletableEntityRepository<Result> resultRepository;

        public ResultService(IDeletableEntityRepository<Result> resultRepository)
        {
            this.resultRepository = resultRepository;
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

            return result.Id;
        }
    }
}
