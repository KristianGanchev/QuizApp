using Quizler.Web.Shared.Models.Answers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quizler.Web.Shared.Models.Questions
{
    public class QuestionRequest
    {
        [Required]
        public string Text { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Points must be positive number")]
        public int Points { get; set; }

        public int QuizId { get; set; }

        public List<AnswerRequest> Answers { get; set; }
    }
}
