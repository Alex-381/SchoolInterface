﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolInterface.Core.Requests
{
    public class AddStudentSubjectRequest
    {
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
    }
}
