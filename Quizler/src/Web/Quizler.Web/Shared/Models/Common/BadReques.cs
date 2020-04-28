namespace Quizler.Web.Shared.Models.Common
{
    using Microsoft.AspNetCore.Mvc;

    public class BadReques : ProblemDetails
    {
        public string Message { get; set; }
    }
}
