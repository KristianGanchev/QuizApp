namespace Quizler.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>();

        Task<int> CreateAsync(string name);

        T GetByName<T>(string name);
    }
}