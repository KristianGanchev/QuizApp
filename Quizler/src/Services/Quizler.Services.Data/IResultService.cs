namespace Quizler.Services.Data
{
    using Quizler.Web.Shared.Models.Answers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IResultService
    {
        Task<int> CreateAync(int points, int maxPoints, string studentId, int quizId, List<AnswerResponse> answers);

        T GetById<T>(int resultId);

        T GetByUserAndQuizId<T>(string userId, int quizId);

        IEnumerable<T> GetAllByUser<T>(string userId);

        Task<int> DeleteAsync(int id);
    }
}
