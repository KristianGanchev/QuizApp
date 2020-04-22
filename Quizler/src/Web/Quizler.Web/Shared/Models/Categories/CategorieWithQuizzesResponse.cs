namespace Quizler.Web.Shared.Models.Categories
{
    using AutoMapper;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Quizzes;
    using System.Collections.Generic;

    public class CategorieWithQuizzesResponse : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public IEnumerable<QuizIndexResponse> Quizzes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategorieWithQuizzesResponse>()
                  .ForMember(q => q.Quizzes, options =>
                  {
                      options.Ignore();
                  });
        }
    }
}
