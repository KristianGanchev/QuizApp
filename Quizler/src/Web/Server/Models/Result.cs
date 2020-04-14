namespace Quizler.Server.Models
{
    using Quizler.Server.Common.Models;

    public class Result : BaseModel<int>
    {
        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
