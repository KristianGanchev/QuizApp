namespace Quizler.Web.Server.Authentication
{
    using Quizler.Data.Models;
    public interface IJwtAuthenticationService
    {
        string Authenticate(ApplicationUser user);
    }
}
