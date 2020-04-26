namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAnswersServices
    {
        Task<int> CreateAync(string name, bool isCorrect, int questionId);

        Task<int> UpdateAsync(string text, bool isCorrect, int id);

        IEnumerable<T> GetAll<T>(int id);
    }
}
