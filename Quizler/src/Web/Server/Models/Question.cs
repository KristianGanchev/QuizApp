namespace Quizler.Server.Models
{
    using Quizler.Server.Common.Models;
    using System.Collections.Generic;

    public class Question : BaseModel<int>
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
