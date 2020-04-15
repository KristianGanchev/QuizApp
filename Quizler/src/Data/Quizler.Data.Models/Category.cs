namespace Quizler.Data.Models
{
    using Quizler.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
