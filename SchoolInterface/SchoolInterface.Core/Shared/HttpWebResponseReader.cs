using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolInterface.Core.Shared
{
    public class HttpWebResponseReader
    {
        public string Read(HttpWebResponse httpResponse)
        {
            try
            {
                StreamReader reader = new StreamReader(httpResponse.GetResponseStream());
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
