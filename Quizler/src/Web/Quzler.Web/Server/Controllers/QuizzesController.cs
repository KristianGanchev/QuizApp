
namespace Quzler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Data.Models;
    using Quizler.Services.Data;
    using Quzler.Web.Shared.Quizzes;
    using System.Security.Claims;
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
        public async Task<ActionResult<QuizCreateResponseModel>> Create([FromBody] QuizCreateRequestModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var quizId = await this.quizzesService.CreateAsync(model.Name, model.CategorieId, userId);

            return new QuizCreateResponseModel { Id = quizId, Name = model.Name};
        }

        //[HttpGet("[acton]")]
        //[AllowAnonymous]
        //public async Task<ActionResult<QuizEditResponseModel>> GetById(int id)
        //{
        //    var quizId = this.quizzesService.GetByIdAsync(id);

        //}
    }
}
