namespace Quizler.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Data.Models;
    using Quizler.Services.Data;
    using Quizler.Web.ViewModels.Quizzes;

    public class QuizzesController : BaseController
    {
        private readonly IQuizzesService quizzesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuizzesController(IQuizzesService quizzesService, ICategoriesService categoriesService, UserManager<ApplicationUser> userManager)
        {
            this.quizzesService = quizzesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoriesDropdownViewModel>();

            var viewModel = new QuizCreateInputModel()
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(QuizCreateInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var quizId = await this.quizzesService.CreateAsync(inputModel.Name, inputModel.CategoryId, user.Id);

            this.TempData["InfoMessage"] = "Quiz created!";

            return this.RedirectToAction(nameof(this.Edit), new { id = quizId });
        }


        public IActionResult Edit()
        {
            return this.View();
        }
    }
}
