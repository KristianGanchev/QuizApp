namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuizzesService
    {
        Task<int> CreateAsync(string name, int categoryId, string userId);

        Task<int> GetByIdAsync(int id);

        IEnumerable<T> GetAll<T>();
    }
}
