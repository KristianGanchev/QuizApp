namespace Quizler.Server.Models
{
    using Quizler.Server.Common.Models;
    using System.Collections.Generic;

    public class Quiz : BaseModel<int>
    {
        public Quiz()
        {
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Plays { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
