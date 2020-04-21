namespace Quizler.Services.Data
{
    using Quizler.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuizzesService
    {
        Task<int> CreateAsync(string name, int categoryId, ApplicationUser user);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();
    }
}
