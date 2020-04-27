namespace Quizler.Web.Shared.Models.Answers
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class AnswerResultDetails : IMapFrom<Answer>
    {
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsSelected { get; set; }
    }
}
