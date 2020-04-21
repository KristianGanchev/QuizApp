namespace Quzler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using Quzler.Web.Shared.Questions;
    using System.Threading.Tasks;

    public class QuestionController : ApiCotroller
    {
        private readonly IQuestionsServices questionsServices;

        public QuestionController(IQuestionsServices questionsServices)
        {
            this.questionsServices = questionsServices;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<QuestionResponseModel>> Create([FromBody] QuestionRequestModel model) 
        {
            var questionId = await this.questionsServices.CreateAsync(model.Text, model.Points, model.quizId);

            return new QuestionResponseModel { Text = model.Text, Id = questionId };
        }
    }
}
