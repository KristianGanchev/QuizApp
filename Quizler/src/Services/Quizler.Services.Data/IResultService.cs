namespace Quizler.Services.Data
{
    using System.Threading.Tasks;

    public interface IResultService
    {
        Task<int> CreateAync(int points, int maxPoints, string studentId, int quizId);

        T GetByQuizId<T>(int quizId);
    }
}
