using Newtonsoft.Json;
using SchoolInterface.Core.Models;
using SchoolInterface.Core.Requests;
using SchoolInterface.Core.Results;
using SchoolInterface.Core.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SchoolInterface.Core.Services
{
    public class StudentService
    {
        private HttpWebResponseReader httpWebResponseReader = new HttpWebResponseReader();

        public List<Student> Get()
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "student"
            };

            var httpWebResponse = httpRequest.Get();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<List<Student>>(details);
        }

        public MessageResult Save(Student student)
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "student/Save",
                data = JsonConvert.SerializeObject(student)
            };

            var httpWebResponse = httpRequest.Post();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<MessageResult>(details);
        }

        public MessageResult Delete(int StudentID)
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "student/Delete/" + StudentID
            };

            var httpWebResponse = httpRequest.Get();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<MessageResult>(details);
        }

        public List<Subject> GetSubjects(int StudentID)
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "student/Subjects/" + StudentID
            };

            var httpWebResponse = httpRequest.Get();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<List<Subject>>(details);
        }

        public MessageResult AddSubject(AddStudentSubjectRequest request)
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "student/AddSubject",
                data = JsonConvert.SerializeObject(request)
            };

            var httpWebResponse = httpRequest.Post();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<MessageResult>(details);
        }

        public MessageResult DeleteSubject(DeleteStudentSubjectRequest request)
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "student/DeleteSubject",
                data = JsonConvert.SerializeObject(request)
            };

            var httpWebResponse = httpRequest.Post();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<MessageResult>(details);
        }
    }
}
