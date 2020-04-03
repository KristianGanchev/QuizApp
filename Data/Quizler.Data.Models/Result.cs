namespace Quizler.Data.Models
{

    using Quizler.Data.Common.Models;

    public class Result : BaseDeletableModel<int>
    {
        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
