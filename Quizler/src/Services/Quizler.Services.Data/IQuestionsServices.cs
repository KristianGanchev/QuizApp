namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuestionsServices
    {
        Task<int> CreateAsync(string name, int points, int quizId);

        Task<int> UpdateAsync(string text, int points, int id);

        IEnumerable<T> GetAll<T>(int id);

        T GetByQuizId<T>(int quizId); 
    }
}
