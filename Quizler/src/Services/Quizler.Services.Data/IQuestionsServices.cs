namespace Quizler.Services.Data
{
    using System.Threading.Tasks;

    public interface IQuestionsServices
    {
        Task<int> CreateAsync(string name, int points, int quizId);
    }
}
