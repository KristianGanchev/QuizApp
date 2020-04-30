namespace Quizler.Web.Client.Services
{
    using Quizler.Web.Shared.Models.Areas.Administration;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<T> GetAll<T>(string route);

        Task CreateAsync(CategoryRequest category);

        Task<T> GetByName<T>(string route);
    }
}
