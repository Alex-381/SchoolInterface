using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SchoolInterface.Core.Shared
{
    public class HttpMethodRequest
    {
        public string baseUrl = "https://localhost:44327/";
        public string url { get; set; }
        public string data { get; set; }

        public HttpWebResponse Post()
        {
            const string method = "Post";
            var encoding = new ASCIIEncoding();

            byte[] bytes = null;

            if (data != null)
            {
                bytes = encoding.GetBytes(data);
            }

            var uri = baseUrl + url;

            var request = WebRequest.Create(uri);
            request.Method = method;
            request.ContentType = "application/json";

            if (data != null)
            {
                request.ContentLength = bytes.Length;
            }

            var stream = request.GetRequestStream();

            if (data != null)
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch(WebException wex)
            {
                HttpWebResponse httpResponse = wex.Response as HttpWebResponse;

                if (httpResponse != null)
                {
                    throw new ApplicationException(string.Format(
                        "Remote server call {0} {1} resulted in a http error {2} {3}.",
                        method,
                        uri,
                        httpResponse.StatusCode,
                        httpResponse.StatusDescription), wex);
                }
                else
                {
                    throw new ApplicationException(string.Format(
                        "Remote server call {0} {1} result in an error.",
                        method,
                        uri), wex);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpWebResponse Get()
        {
            const string method = "Get";
            var encoding = new ASCIIEncoding();

            var uri = baseUrl + url;

            var request = WebRequest.Create(uri);
            request.Method = method;
            request.ContentType = "application/json";

            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException wex)
            {
                HttpWebResponse httpResponse = wex.Response as HttpWebResponse;

                if (httpResponse != null)
                {
                    throw new ApplicationException(string.Format(
                        "Remote server call {0} {1} resulted in a http error {2} {3}.",
                        method,
                        uri,
                        httpResponse.StatusCode,
                        httpResponse.StatusDescription), wex);
                }
                else
                {
                    throw new ApplicationException(string.Format(
                        "Remote server call {0} {1} result in an error.",
                        method,
                        uri), wex);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
