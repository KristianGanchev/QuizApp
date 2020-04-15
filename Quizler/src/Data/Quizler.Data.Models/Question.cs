namespace Quizler.Data.Models
{
    using System.Collections.Generic;

    using Quizler.Data.Common.Models;

    public class Question : BaseDeletableModel<int>
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public string Text { get; set; }

        public int Points { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
