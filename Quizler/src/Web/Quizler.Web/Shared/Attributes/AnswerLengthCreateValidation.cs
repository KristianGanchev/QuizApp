namespace Quizler.Web.Shared.Attributes
{
    using Quizler.Web.Shared.Models.Questions;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AnswerLengthCreateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var question = (QuestionCreateRequest)validationContext.ObjectInstance;

            if (question.Answers.Any(a => string.IsNullOrEmpty(a.Text))  || question.Answers.Any(a => string.IsNullOrWhiteSpace(a.Text)))
            {
                return new ValidationResult("Asnwers must be 4!");
            }

            return ValidationResult.Success;
        }
    }
}
