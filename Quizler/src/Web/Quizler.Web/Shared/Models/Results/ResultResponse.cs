using Quizler.Data.Models;
using Quizler.Services.Mapping;

namespace Quizler.Web.Shared.Models.Results
{
    public class ResultResponse : IMapFrom<Result>
    {
        public int Id { get; set; }
    }
}
