namespace Quizler.Server.Models
{
    using Quizler.Server.Common.Models;

    public class Category : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
