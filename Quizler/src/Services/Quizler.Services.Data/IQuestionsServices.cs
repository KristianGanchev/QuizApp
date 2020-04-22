namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuestionsServices
    {
        Task<int> CreateAsync(string name, int points, int quizId);

        IEnumerable<T> GetAll<T>(int id);
    }
}
