namespace Quizler.Web.Client.Services
{
    using Quizler.Web.Shared.Models.Answers;
    using Quizler.Web.Shared.Models.Questions;
    using Quizler.Web.Shared.Models.Quizzes;
    using System.Threading.Tasks;

    public interface IQuizzesService
    {
        Task CreateAsync(QuizCreateRequest Quiz);

        Task UpdateAsync(QuizEditResponse quiz);

        Task DeleteAsync(int quizId);

        Task<T> GetById<T>(int quizId, string rout);

        Task<T> Search<T>(string searchQuery);

        Task<T> GetAllByUser<T>();
    }
}
