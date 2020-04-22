using Quizler.Data.Models;
using Quizler.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizler.Web.Shared.Models.Answers
{
    public class AnswerResponse : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
