namespace Quizler.Web.Shared.Attributes
{
    using Quizler.Web.Shared.Models.Questions;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AnswerIsCorrectEditValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var question = (QuestionEditResponse)validationContext.ObjectInstance;

            if (question.Answers.Any(a => a.IsCorrect) == false)
            {
                return new ValidationResult("Select a correct answer");
            }

            return ValidationResult.Success;
        }
    }
}
