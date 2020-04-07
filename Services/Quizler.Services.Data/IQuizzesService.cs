namespace Quizler.Services.Data
{
    using System.Threading.Tasks;

    public interface IQuizzesService
    {
        Task<int> CreateAsync(string name, int categoryId, string userId);
    }
}
