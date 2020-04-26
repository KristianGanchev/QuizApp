using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Answers;
    using System.Threading.Tasks;

    public class AnswersController : ApiController
    {
        private readonly IAnswersServices answersServices;

        public AnswersController(IAnswersServices answersServices)
        {
            this.answersServices = answersServices;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<AnswerResponse>> Create([FromBody] AnswerCreateRequest model)
        {
            var asnwerId = await this.answersServices.CreateAync(model.Text, model.IsCorrect, model.QuestionId);

            return new AnswerResponse { Id = asnwerId, Text = model.Text };
        }


        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<AnswerResponse>> Update([FromBody] AnswerEditResponse model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var questionId = await this.answersServices.UpdateAsync(model.Text, model.IsCorrect, model.Id);

            return new AnswerResponse { Text = model.Text, Id = questionId };
        }

        [HttpGet("[action]/{questionId}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<AnswerResponse>> GetAll(int questionId)
        {

            return this.answersServices.GetAll<AnswerResponse>(questionId).ToArray();

        }
    }
}
