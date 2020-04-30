namespace Quizler.Web.Shared.Attributes
{
    using Quizler.Web.Shared.Models.Quizzes;
    using System.ComponentModel.DataAnnotations;

    public class QuetionLengthValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var quiz = (QuizCreateRequest)validationContext.ObjectInstance;

            if (quiz.Questions.Count < 4)
            { 
                return new ValidationResult("Questions must be atleast 4!");
            }

            return ValidationResult.Success;
        }
    }
}
