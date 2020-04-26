namespace Quizler.Data.Models
{
    using Quizler.Data.Common.Models;
    using System.Collections.Generic;

    public class Answer : BaseDeletableModel<int>
    {
        public Answer()
        {
            this.Results = new HashSet<AnswerResult>();
        }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<AnswerResult> Results { get; set; }
    }
}
