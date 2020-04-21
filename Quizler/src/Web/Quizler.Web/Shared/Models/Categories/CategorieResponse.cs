
using Quizler.Data.Models;
using Quizler.Services.Mapping;

namespace Quizler.Web.Shared.Models.Categories
{
    public class CategorieResponse : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
