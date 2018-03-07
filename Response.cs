using System;
using System.Collections.Generic;
using NetJSON;

namespace nuclio_sdk_dotnetcore
{
    public class Response
    {
        [NetJSONProperty("body")]
        public string Body { get; set; }

        [NetJSONProperty("content_type")]
        public string ContentType { get; set; }
        
        [NetJSONProperty("status_code")]
        public int StatusCode { get; set; }

        [NetJSONProperty("headers")]
        public Dictionary<string, object> Headers { get; set; }

        [NetJSONProperty("body_encoding")]
        public string BodyEncoding { get; set; }

        public Response()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Headers = new Dictionary<string, object>(comparer);
        }

    }
}