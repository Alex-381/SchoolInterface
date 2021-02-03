using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolInterface.Core.Results
{
    public class MessageResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
