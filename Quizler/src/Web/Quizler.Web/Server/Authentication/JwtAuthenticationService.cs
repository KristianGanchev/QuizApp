namespace Quizler.Web.Server.Authentication
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Quizler.Common;
    using Quizler.Data.Models;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly JwtSettings jwtSettings;
        private readonly UserManager<ApplicationUser> userManager;

        public JwtAuthenticationService(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager)
        {
            this.jwtSettings = jwtSettings.Value;
            this.userManager = userManager;
        }

        public string Authenticate(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role,
                            this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName)
                                .GetAwaiter()
                                .GetResult() ? GlobalConstants.AdministratorRoleName : "User")
                    }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
