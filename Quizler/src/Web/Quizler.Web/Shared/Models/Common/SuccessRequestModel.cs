namespace Quizler.Web.Shared.Models.Common
{
    public class SuccessRequestModel<T>
    {
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
