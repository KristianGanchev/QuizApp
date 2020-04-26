namespace Quizler.Services.Data
{
    using Quizler.Web.Shared.Models.Answers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IResultService
    {
        Task<int> CreateAync(int points, int maxPoints, string studentId, int quizId, List<AnswerResponse> answers);

        T GetByQuizId<T>(int quizId);
    }
}
