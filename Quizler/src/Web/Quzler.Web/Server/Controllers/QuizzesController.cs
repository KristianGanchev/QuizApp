
namespace Quzler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Data.Models;
    using Quizler.Services.Data;
    using Quzler.Web.Shared.Quizzes;
    using System.Threading.Tasks;

    //[Authorize]
    public class QuizzesController : ApiCotroller
    {
        private readonly IQuizzesService quizzesService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuizzesController(IQuizzesService quizzesService, UserManager<ApplicationUser> userManager)
        {
            this.quizzesService = quizzesService;
            this.userManager = userManager;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Create([FromBody] QuizCreateModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var quizId = await this.quizzesService.CreateAsync(model.Name, model.CategorieId, user.Id);

            return quizId;
        }
    }
}
