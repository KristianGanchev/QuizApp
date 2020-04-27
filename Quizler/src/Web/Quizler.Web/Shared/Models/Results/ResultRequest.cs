namespace Quizler.Web.Shared.Models.Results
{
    using Quizler.Web.Shared.Models.Answers;
    using System.Collections.Generic;
    public class ResultRequest
    {
        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public int QuizId { get; set; }

        public ICollection<AnswerResponse> MyAnswers { get; set; }
    }
}
