using Quizler.Web.Shared.Models.Categories;
using Quizler.Web.Shared.Models.Quizzes;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public interface IApiService
    {
        Task<CategorieResponse[]> GetCategoriesNames();

        Task<QuizResponse> CreateQuiz(QuizCreateRequest request);

        Task<QuizEditResponse> GetQuizById(int id);
    }
}