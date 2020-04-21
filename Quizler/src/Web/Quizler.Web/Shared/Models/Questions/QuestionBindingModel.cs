namespace Quizler.Web.Shared.Models.Questions
{
    using Quizler.Web.Shared.Models.Answers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class QuestionBindingModel
    {
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Question")]
        public string Text { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} must be bigger than 0")]
        [Display(Name = "Points")]
        public int Points { get; set; }

        [Required]
        public AnswerRequest FirstAnswer { get; set; }

        [Required]
        public AnswerRequest SecondAnswer { get; set; }

        [Required]
        public AnswerRequest ThirdAnswer { get; set; }

        [Required]
        public AnswerRequest FourthAnswer { get; set; }
    }
}
