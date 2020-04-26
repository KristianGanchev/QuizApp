namespace Quizler.Web.Shared.Models.Answers
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class AnswerEditResponse : IMapFrom<Answer>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Answer")]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
