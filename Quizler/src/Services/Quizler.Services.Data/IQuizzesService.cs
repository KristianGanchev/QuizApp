namespace Quizler.Services.Data
{
    using Quizler.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuizzesService
    {
        Task<int> CreateAsync(string name, int categoryId, ApplicationUser user, string imageUrl);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByUser<T>(string userId);

        IEnumerable<T> GetByCategory<T>(string categoryName);

        Task<int> UpdateAsync(string name, int categoryId, string imageUrl, int id);

        Task<int> DeleteAsync(int id);
    }
}
