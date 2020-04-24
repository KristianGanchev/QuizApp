namespace Quizler.Data.Models
{
    using System.Collections.Generic;

    using Quizler.Data.Common.Models;

    public class Quiz : BaseDeletableModel<int>
    {
        public Quiz()
        {
            this.Questions = new HashSet<Question>();
            this.Results = new HashSet<Result>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Plays { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
