namespace Quizler.Services.Data
{
    using System.Threading.Tasks;

    public interface IAnswersServices
    {
        Task<int> CreateAync(string name, bool isCorrect, int questionId);
    }
}
