namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Data.Models;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Quizzes;
    using Quizler.Web.Shared.Models.Results;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ResultsController : ApiController
    {
        private readonly IResultService resultService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IQuizzesService quizzesService;

        public ResultsController(IResultService resultService, UserManager<ApplicationUser> userManager, IQuizzesService quizzesService)
        {
            this.resultService = resultService;
            this.userManager = userManager;
            this.quizzesService = quizzesService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResultResponse>> Create([FromBody] ResultRequest resultRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userName = this.User.Identity.Name;
            var user = this.userManager.Users.SingleOrDefault(u => u.UserName == userName);

            var quiz = this.quizzesService.GetById<QuizPlayResponse>(resultRequest.QuizId);

            var maxPoints = quiz.Questions.Sum(q => q.Points);

            var resultId = await this.resultService.CreateAync(resultRequest.Points, maxPoints, user.Id, resultRequest.QuizId, resultRequest.MyAnswers.ToList());

            return new ResultResponse { Id = resultId };
        }

        [HttpGet("{resultId}")]
        public ActionResult<ResultDetailsResponse> GetById(int resultId)
        {

            var result = this.resultService.GetById<ResultDetailsResponse>(resultId);

            return result;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<ResultsMyResponse>> UserResults() 
        {
            var userName = this.User.Identity.Name;
            var user = this.userManager.Users.SingleOrDefault(u => u.UserName == userName);


            var results = this.resultService.GetAllByUser<ResultsMyResponse>(user.Id).ToList();

            return results;
        }

        [HttpDelete("[action]/{id}")]
        public async Task<int> Delete([FromRoute] int id)
        {
            return await this.resultService.DeleteAsync(id);
        }
    }
}
