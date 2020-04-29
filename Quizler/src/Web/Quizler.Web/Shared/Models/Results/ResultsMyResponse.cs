

namespace Quizler.Web.Shared.Models.Results
{
    using AutoMapper;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class ResultsMyResponse : IMapFrom<Result>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public string Quiz { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Result, ResultsMyResponse>()
                .ForMember(r => r.Quiz, options =>
                {
                    options.MapFrom(r => r.Quiz.Name);

                });
        }
    }
}
