using Quizler.Data.Models;
using Quizler.Services.Mapping;
using Quizler.Web.Shared.Models.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizler.Web.Shared.Models.Results
{
    public class ResultAnswersResponse : IMapFrom<AnswerResult>
    {
        public AnswerResponse Answer { get; set; }
    }
}
