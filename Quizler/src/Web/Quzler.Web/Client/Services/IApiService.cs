namespace Quzler.Web.Client.Services
{
    using Quzler.Web.Shared.Categories;
    using Quzler.Web.Shared.Quizzes;
    using System.Threading.Tasks;
    public interface IApiService
    {
        public Task<CategorieResponseModel[]> GetCategoriesNames();

        Task<QuizCreateResponseModel> CreateQuiz(QuizCreateRequestModel request);
    }
}
