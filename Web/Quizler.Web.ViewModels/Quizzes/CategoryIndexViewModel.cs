namespace Quizler.Web.ViewModels.Quizzes
{

    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class CategoryIndexViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
