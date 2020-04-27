namespace Quizler.Web.Shared.Models.Questions
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Answers;
    using System.Collections.Generic;

    public class QuestionsResultDetails : IMapFrom<Question>
    {
        public string Text { get; set; }

        public List<AnswerResultDetails> Answers { get; set; }
    }
}
