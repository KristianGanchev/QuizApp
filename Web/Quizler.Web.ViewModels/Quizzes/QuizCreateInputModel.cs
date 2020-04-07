namespace Quizler.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class QuizCreateInputModel : IMapTo<Quiz>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CreatorId { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesDropdownViewModel> Categories { get; set; }
    }
}
