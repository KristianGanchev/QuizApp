namespace Quizler.Web.Shared.Models.Results
{
    using AutoMapper;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Quizzes;
    using System.Collections.Generic;

    public class ResultDetailsResponse : IMapFrom<Result>
    {
        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public QuizResultDetails Quiz { get; set; }

        public List<ResultAnswersResponse> SelectedAnswers { get; set; }
    }
}
