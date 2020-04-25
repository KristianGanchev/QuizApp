namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using System.Collections.Generic;
    using System.Linq;
    using Quizler.Web.Shared.Models.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Quizler.Web.Shared.Models.Quizzes;

    [AllowAnonymous]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IQuizzesService quizzesService;

        public CategoriesController(ICategoriesService categoriesService, IQuizzesService quizzesService)
        {
            this.categoriesService = categoriesService;
            this.quizzesService = quizzesService;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<CategorieResponse>> GetAll()
        {

            return this.categoriesService.GetAll<CategorieResponse>().ToArray();

        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<CategorieWithQuizzesResponse>> GetAllWithQuizzes()
        {

            var categories = this.categoriesService.GetAll<CategorieWithQuizzesResponse>();

            foreach (var category in categories)
            {
                var quizzes = this.quizzesService.GetByCategory<QuizAllResponse>(category.Name);

                category.Quizzes = quizzes;
            }

            return categories.ToArray();
        }
    }
}
