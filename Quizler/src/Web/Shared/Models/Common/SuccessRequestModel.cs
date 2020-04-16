using System;
using System.Collections.Generic;
using System.Text;

namespace Quizler.Shared.Models.Common
{
    public class SuccessRequestModel<T>
    {
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
