namespace Quizler.Data.Models
{
    public class AnswerResult
    {
        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

        public int ResultId { get; set; }

        public Result Result { get; set; }
    }
}
