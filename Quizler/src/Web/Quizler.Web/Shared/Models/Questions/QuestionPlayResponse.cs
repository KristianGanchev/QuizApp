namespace Quizler.Web.Shared.Models.Questions
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Answers;
    using System.Collections.Generic;

    public class QuestionPlayResponse : IMapFrom<Question>
    {
        public string Text { get; set; }

        public int Points { get; set; }

        public IEnumerable<AnswerResponse> Answers { get; set; }
    }
}
