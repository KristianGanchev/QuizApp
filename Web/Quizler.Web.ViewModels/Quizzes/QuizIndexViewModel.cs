namespace Quizler.Web.ViewModels.Quizzes
{
    using AutoMapper;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class QuizIndexViewModel : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public CategoryIndexViewModel Category { get; set; }

        public int Plays { get; set; }

        public int Questions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Quiz, QuizIndexViewModel>()
                .ForMember(q => q.Questions, options =>
                {
                    options.MapFrom(q => q.Questions.Count);
                });
        }
    }
}
