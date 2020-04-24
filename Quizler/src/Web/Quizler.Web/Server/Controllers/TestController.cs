using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quizler.Data.Models;
using Quizler.Services.Data;
using Quizler.Web.Shared.Models.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizler.Web.Server.Controllers
{
    [AllowAnonymous]
    public class TestController : ApiController
    {
        private readonly IQuizzesService quizzesService;
        private readonly IQuestionsServices questionsServices;
        private readonly IAnswersServices answersServices;
        private readonly UserManager<ApplicationUser> userManager;

        public TestController(IQuizzesService quizzesService, IQuestionsServices questionsServices, IAnswersServices answersServices , UserManager<ApplicationUser> userManager)
        {
            this.quizzesService = quizzesService;
            this.questionsServices = questionsServices;
            this.answersServices = answersServices;
            this.userManager = userManager;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<QuizPlayResponse> GetById(int id)
        {
            var quiz = this.quizzesService.GetById<QuizPlayResponse>(id);

            return quiz;
        }
    }
}
