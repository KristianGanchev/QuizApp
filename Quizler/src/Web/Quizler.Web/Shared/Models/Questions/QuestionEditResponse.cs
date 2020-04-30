namespace Quizler.Web.Shared.Models.Questions
{
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Attributes;
    using Quizler.Web.Shared.Models.Answers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class QuestionEditResponse : IMapFrom<Question>
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Points must be positive number")]
        public int Points { get; set; }

        [AnswerLengthEditValidation]
        [AnswerIsCorrectEditValidation]
        [Display(Name = "Answers")]
        public List<AnswerEditResponse> Answers { get; set; }
    }
}
