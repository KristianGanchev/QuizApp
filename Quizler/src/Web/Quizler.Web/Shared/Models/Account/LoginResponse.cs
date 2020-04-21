namespace Quizler.Web.Shared.Models.Account
{
    public class LoginResponseModel
    {
        public string access_token { get; set; }
        public bool Successful { get; set; }
        public string Error { get; set; }
    }
}
