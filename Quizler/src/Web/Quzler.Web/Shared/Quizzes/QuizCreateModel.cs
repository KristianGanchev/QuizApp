namespace Quzler.Web.Shared.Quizzes
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quzler.Web.Shared.Categories;
    using System.ComponentModel.DataAnnotations;

    public class QuizCreateModel : IMapTo<Quiz>
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Quiz name")]
        public string Name { get; set; }

        [Required]
        public int CategorieId { get; set; }

        public string UserId { get; set; }
    }
}
