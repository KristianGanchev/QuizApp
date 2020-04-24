namespace Quizler.Web.Shared.Models.Results
{
    public class ResultRequest
    {
        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public string User { get; set; }

        public int QuizId { get; set; }
    }
}
