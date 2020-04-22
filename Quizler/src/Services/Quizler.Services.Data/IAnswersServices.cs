namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAnswersServices
    {
        Task<int> CreateAync(string name, bool isCorrect, int questionId);

        IEnumerable<T> GetAll<T>(int id);
    }
}
