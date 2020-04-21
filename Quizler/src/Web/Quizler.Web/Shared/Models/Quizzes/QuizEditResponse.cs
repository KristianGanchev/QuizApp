namespace Quizler.Web.Shared.Models.Quizzes
{
    using AutoMapper;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class QuizEditResponse : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Quiz, QuizEditResponse>()
                .ForMember(q => q.Category, options =>
                {
                    options.MapFrom(q => q.Category.Name);
                });
        }
    }
}
