using Quizler.Web.Shared.Models.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Quizler.Web.Shared.Attributes
{
    public class AsnwerIsCorrectCreateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var question = (QuestionCreateRequest)validationContext.ObjectInstance;

            if (question.Answers.Any(a => a.IsCorrect) == false)
            {
                return new ValidationResult("Select a correct answer");
            }

            return ValidationResult.Success;
        }
    }
}
