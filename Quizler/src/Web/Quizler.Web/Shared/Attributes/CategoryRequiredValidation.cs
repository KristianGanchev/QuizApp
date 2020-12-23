namespace Quizler.Web.Shared.Attributes
{
    using Quizler.Web.Shared.Models.Quizzes;
    using System.ComponentModel.DataAnnotations;

    public class CategoryRequiredValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var quiz = (QuizCreateRequest)validationContext.ObjectInstance;

            if (quiz.CategorieId == 0)
            {
                return new ValidationResult("Category is required!");
            }

            return ValidationResult.Success;
        }
    }
}
