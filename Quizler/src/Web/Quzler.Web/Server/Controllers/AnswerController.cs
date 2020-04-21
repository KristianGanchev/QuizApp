namespace Quzler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using Quzler.Web.Shared.Answers;
    using System.Threading.Tasks;

    public class AnswerController : ApiCotroller
    {
        private readonly IAnswersServices answersServices;

        public AnswerController(IAnswersServices answersServices)
        {
            this.answersServices = answersServices;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<AnswerCreateResponseModel>> Create([FromBody] AnswerCreateRequestModel model)
        {
            var asnwerId = await this.answersServices.CreateAync(model.Text, model.IsCorrect, model.QuestionId);

            return new AnswerCreateResponseModel { Id = asnwerId, Text = model.Text };
        }
    }
}
