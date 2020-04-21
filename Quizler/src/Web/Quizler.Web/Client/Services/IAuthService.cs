using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    using Quizler.Web.Shared.Models.Account;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<RegisterResponseModel> Register(RegisterRequestModel registerModel);

        Task<LoginResponseModel> Login(LoginRequestModel loginModel);

        Task Logout();
    }
}
