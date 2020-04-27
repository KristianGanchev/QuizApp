namespace Quizler.Web.Shared.Models.Quizzes
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class QuizResponse : IMapFrom<Quiz>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
