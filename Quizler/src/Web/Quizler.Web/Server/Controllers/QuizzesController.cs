namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Data.Models;
    using Quizler.Services;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Quizzes;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    public class QuizzesController : ApiController
    {
        private readonly IQuizzesService quizzesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public QuizzesController(IQuizzesService quizzesService, UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService)
        {
            this.quizzesService = quizzesService;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<QuizResponse>> Create([FromBody] QuizCreateRequest model)
        {
            var user = this.userManager.Users.SingleOrDefault(u => u.Email == model.User);

            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            
            string imageUrl = await this.cloudinaryService.UploadPictureAsync(
               model.ImageUrl,
               model.Name);

            var quizId = await this.quizzesService.CreateAsync(model.Name, model.CategorieId, user, imageUrl);

           return new QuizResponse { Id = quizId, Name = model.Name };
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public  ActionResult<QuizEditResponse> GetById(int id)
        {
            var quiz = this.quizzesService.GetById<QuizEditResponse>(id);

            return quiz;
        }

        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public ActionResult<QuizPlayResponse> Play(int id)
        {
            var quiz = this.quizzesService.GetById<QuizPlayResponse>(id);

            return quiz;
        }
    }
}
