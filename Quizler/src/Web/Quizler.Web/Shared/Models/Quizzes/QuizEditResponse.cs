namespace Quizler.Web.Shared.Models.Quizzes
{
    using AutoMapper;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Questions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class QuizEditResponse : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Quiz name")]
        public string Name { get; set; }

        [Required]
        public int CategorieId { get; set; }

        public string ImageUrl { get; set; }

        public List<QuestionEditResponse> Questions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Quiz, QuizEditResponse>()
                .ForMember(q => q.CategorieId, options =>
                {
                    options.MapFrom(q => q.Category.Id);

                });
        }
    }
}
