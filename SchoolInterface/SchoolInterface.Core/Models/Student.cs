using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolInterface.Core.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int StartYear { get; set; }
        public Address Address { get; set; }
        public Guardian Guardian { get; set; }
    }
}
