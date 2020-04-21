namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Data.Models;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Quizzes;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class QuizzesController : ApiController
    {
        private readonly IQuizzesService quizzesService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuizzesController(IQuizzesService quizzesService, UserManager<ApplicationUser> userManager)
        {
            this.quizzesService = quizzesService;
            this.userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<QuizResponse>> Create([FromBody] QuizCreateRequest model)
        {
            var user = this.userManager.Users.SingleOrDefault(u => u.Email == model.User);

            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var quizId = await this.quizzesService.CreateAsync(model.Name, model.CategorieId, user);

            return new QuizResponse { Id = quizId, Name = model.Name };
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public  ActionResult<QuizEditResponse> GetById(int id)
        {
            var quiz = this.quizzesService.GetById<QuizEditResponse>(id);

            return quiz;
        }
    }
}
