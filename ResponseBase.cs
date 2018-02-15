using System;
using System.Collections.Generic;
using MessagePack;

namespace nuclio_sdk_dotnetcore
{
    public class ResponseBase
    {
        [Key("body")]
        [MessagePackFormatter(typeof(ByteStringFormatter))]
        public string Body { get; set; }
        
        [Key("content_type")]
        public string ContentType { get; set; }
        
        [Key("status_code")]
        public int StatusCode { get; set; }
        
        [Key("headers")]
        public Dictionary<string, object> Headers { get; set; }
        
        [Key("body_encoding")]
        public string BodyEncoding { get; set; }
    }
}