using Newtonsoft.Json;
using SchoolInterface.Core.Models;
using SchoolInterface.Core.Results;
using SchoolInterface.Core.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SchoolInterface.Core.Services
{
    public class SubjectService
    {
        private HttpWebResponseReader httpWebResponseReader = new HttpWebResponseReader();

        public List<Subject> Get()
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "subject"
            };

            var httpWebResponse = httpRequest.Get();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<List<Subject>>(details);
        }

        public MessageResult Save(Subject subject)
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "subject/Save",
                data = JsonConvert.SerializeObject(subject)
            };

            var httpWebResponse = httpRequest.Post();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<MessageResult>(details);
        }

        public MessageResult Delete(int SubjectID)
        {
            var httpRequest = new HttpMethodRequest()
            {
                url = "subject/Delete/" + SubjectID
            };

            var httpWebResponse = httpRequest.Get();
            var details = httpWebResponseReader.Read(httpWebResponse);

            return JsonConvert.DeserializeObject<MessageResult>(details);
        }
    }
}
