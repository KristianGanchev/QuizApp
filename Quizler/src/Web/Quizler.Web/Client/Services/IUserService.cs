namespace Quizler.Web.Client.Services
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<bool> IsInRole(string role);

        Task<T> GetAll<T>();
    }
}
