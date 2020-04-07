namespace Quizler.Web.ViewModels.Answers
{
    public class AnswerInputModel
    {
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}
