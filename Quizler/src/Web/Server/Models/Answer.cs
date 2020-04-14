namespace Quizler.Server.Models
{
    using Quizler.Server.Common.Models;

    public class Answer : BaseModel<int>
    {
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
