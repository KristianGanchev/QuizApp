namespace Quizler.Web.ViewModels.Quizzes
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;

    public class CategoriesDropdownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
