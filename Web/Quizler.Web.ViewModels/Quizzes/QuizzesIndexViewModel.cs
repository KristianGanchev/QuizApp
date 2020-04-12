namespace Quizler.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;

    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class QuizzesIndexViewModel
    {
        public IEnumerable<QuizIndexViewModel> Quizzes { get; set; }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }
    }
}
