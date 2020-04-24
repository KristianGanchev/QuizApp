namespace Quizler.Web.Shared.Models.Answers
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class AnswerResponse : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
