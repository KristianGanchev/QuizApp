namespace Quizler.Web.Shared.Models.Common
{
    using Microsoft.AspNetCore.Mvc;

    public class BadRequestModel : ProblemDetails
    {
        public string Message { get; set; }
    }
}
