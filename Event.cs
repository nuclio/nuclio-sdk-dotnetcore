using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using NetJSON;
namespace nuclio_sdk_dotnetcore
{
    public class Event
    {

        [NetJSONProperty("body")]
        public byte [] Body { get; set; }

        [NetJSONProperty("content-type")]
        public string ContentType { get; set; }

        [NetJSONProperty("headers")]
        public Dictionary<string, object> Headers { get; set; }

        [NetJSONProperty("fields")]
        public Dictionary<string, object> Fields { get; set; }

        [NetJSONProperty("size")]
        public long Size { get; set; }

        [NetJSONProperty("id")]
        public string Id { get; set; }

        [NetJSONProperty("method")]
        public string Method { get; set; }

        [NetJSONProperty("path")]
        public string Path { get; set; }

        [NetJSONProperty("url")]
        public string Url { get; set; }

        [NetJSONProperty("version")]
        public long Version { get; set; }

        [NetJSONProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [NetJSONProperty("trigger")]
        public Trigger Trigger { get; set; }

        public Event()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Trigger = new Trigger();
            Headers = new Dictionary<string, object>(comparer);
            Fields = new Dictionary<string, object>();
        }
        public string GetBody()
        {
            return StringHelpers.DecodeString(this.Body);
        }
        public void SetBody(string body)
        {
            this.Body = StringHelpers.EncodeString(body);
        }
    }

    public class Trigger
    {
        [NetJSONProperty("class")]
        public string Class { get; set; }
        
        [NetJSONProperty("kind")]
        public string Kind { get; set; }
    }
}
