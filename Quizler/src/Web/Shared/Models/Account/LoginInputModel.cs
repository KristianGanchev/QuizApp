namespace Quizler.Shared.Models.Account
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using System.ComponentModel.DataAnnotations;
    public class LoginInputModel : IMapFrom<ApplicationUser>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
