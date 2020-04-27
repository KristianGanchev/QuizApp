namespace Quizler.Web.Client.Services
{
    using Quizler.Web.Shared.Models.Questions;
    using System.Threading.Tasks;

    public interface IQuestionsService
    {
        Task UpdateAsync(QuestionEditResponse question);

        Task DeleteAsync(string questionTitle, int quizId);

        Task<T> GetAllByQuizId<T>(int quizId);
    }
}
