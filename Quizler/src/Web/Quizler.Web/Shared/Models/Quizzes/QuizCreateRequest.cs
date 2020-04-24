namespace Quizler.Web.Shared.Models.Quizzes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class QuizCreateRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Quiz name")]
        public string Name { get; set; }

        [Required]
        public int CategorieId { get; set; }

        public string ImageUrl { get; set; }

        public string User { get; set; }
    }
}
