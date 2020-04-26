namespace Quizler.Web.Shared.Models.Answers
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class AnswerCreateRequest : IMapFrom<Answer>
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Answer")]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}
