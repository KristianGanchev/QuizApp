namespace Quzler.Web.Shared.Quizzes
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quzler.Web.Shared.Questions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class QuizEditResponseModel : IMapFrom<Quiz>
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Quiz name")]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<QuestionRequestModel> Quiestions { get; set; }
    }
}
