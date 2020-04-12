namespace Quizler.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using Quizler.Web.ViewModels;
    using Quizler.Web.ViewModels.Quizzes;

    public class HomeController : BaseController
    {
        private readonly IQuizzesService quizzesService;
        private readonly ICategoriesService categoriesService;

        public HomeController(IQuizzesService quizzesService, ICategoriesService categoriesService)
        {
            this.quizzesService = quizzesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var quizzesFromDb = this.quizzesService.GetAll<QuizIndexViewModel>();

            var categories = this.categoriesService.GetAll<CategoryIndexViewModel>();

            var quizzes = new QuizzesIndexViewModel()
            {
                Quizzes = quizzesFromDb,
                Categories = categories,
            };

            return this.View(quizzes);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
