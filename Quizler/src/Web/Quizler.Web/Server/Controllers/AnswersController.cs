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
        public async Task<ActionResult<AnswerResponse>> Create([FromBody] AnswerRequest model)
        {
            var asnwerId = await this.answersServices.CreateAync(model.Text, model.IsCorrect, model.QuestionId);

            return new AnswerResponse { Id = asnwerId, Text = model.Text };
        }
    }
}
