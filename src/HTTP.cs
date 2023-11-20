using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DeanHelper
{
    public class HTTP
    {
        public class HTTPHeader
        {
            public string Header { get; }
            public string Value { get; }
            public HTTPHeader(string header, string value)
            {
                Header = header;
                Value = value;
            }
        }
        public static async Task<HttpResponseMessage> Get(string uri)
        {
            return await Get(uri, new List<HTTPHeader>());
        }
        /// <summary>
        /// Helper Function to make HTTP Get Requests
        /// </summary>
        /// <param name="uri">URI of HTTP GET Request</param>
        /// <param name="headerList">HTTP Headers</param>
        /// <returns>HTTPResponseMessage</returns>
        public static async Task<HttpResponseMessage> Get(string uri, List<HTTPHeader> headerList)
        {
#if DEBUG
            Console.WriteLine(String.Format("HTTP GET request to: {0}", uri));
#endif
            HttpClient httpClient = new HttpClient();

            foreach (HTTPHeader header in headerList)
            {
                httpClient.DefaultRequestHeaders.Add(header.Header, header.Value);
            }

            return await httpClient.GetAsync(new Uri(uri).ToString());
        }
    }
}
