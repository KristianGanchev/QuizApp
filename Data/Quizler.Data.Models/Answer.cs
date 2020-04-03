namespace Quizler.Data.Models
{
    using Quizler.Data.Common.Models;

    public class Answer : BaseDeletableModel<int>
    {
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
