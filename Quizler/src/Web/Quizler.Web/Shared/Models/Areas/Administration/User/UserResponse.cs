namespace Quizler.Web.Shared.Models.Areas.Administration.User
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using System.Linq;
    using System.Security.Claims;

    public class UserResponse : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
