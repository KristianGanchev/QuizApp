using Quizler.Data.Models;
using Quizler.Services.Mapping;
using Quzler.Web.Shared.Answers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quzler.Web.Shared.Questions
{
    public class QuestionRequestModel : IMapFrom<Question>
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Question")]
        public string Text { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} must be a bigger than 0")]
        [Display(Name = "Points")]
        public int Points { get; set; }

       // public IEnumerable<AnswerCreateRequestModel> Answers { get; set; }

        public int quizId { get; set; }
    }
}
