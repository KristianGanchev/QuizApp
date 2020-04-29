namespace Quizler.Web.Shared.Models.Common
{
    using Microsoft.AspNetCore.Mvc;

    public class BadRequest : ProblemDetails
    {
        public string Message { get; set; }
    }
}
