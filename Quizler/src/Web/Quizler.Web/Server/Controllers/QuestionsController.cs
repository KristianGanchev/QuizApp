
namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Questions;
    using System.Threading.Tasks;

    public class QuestionsController : ApiController
    {
        private readonly IQuestionsServices questionsServices;

        public QuestionsController(IQuestionsServices questionsServices)
        {
            this.questionsServices = questionsServices;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<QuestionResponse>> Create([FromBody] QuestionRequest model)
        {
            var questionId = await this.questionsServices.CreateAsync(model.Text, model.Points, model.QuizId);

            return new QuestionResponse { Text = model.Text, Id = questionId };
        }
    }
}
