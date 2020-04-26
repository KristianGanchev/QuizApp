namespace Quizler.Data.Models
{
    using Quizler.Data.Common.Models;
    using System.Collections.Generic;

    public class Result : BaseDeletableModel<int>
    {
        public Result()
        {
            this.SelectedAnswers = new HashSet<AnswerResult>();
        }

        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public virtual int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<AnswerResult> SelectedAnswers { get; set; }
    }
}
