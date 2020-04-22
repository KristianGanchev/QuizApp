namespace Quizler.Web.Shared.Models.Questions
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Answers;
    using System.Collections.Generic;

    public class QuestionResponse : IMapFrom<Question>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Points { get; set; }

        public AnswerResponse[] Answers { get; set; }
    }
}
