//  Copyright 2017 The Nuclio Authors.
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Utf8Json;
using Utf8Json.Formatters;

namespace Nuclio.Sdk
{
    public class Event
    {

        [DataMember(Name = "body")]
        public byte[] Body { get; set; }

        [DataMember(Name = "content-type")]
        public string ContentType { get; set; }

        [DataMember(Name = "headers")]
        public Dictionary<string, object> Headers { get; set; }

        [DataMember(Name = "fields")]
        public Dictionary<string, object> Fields { get; set; }

        [DataMember(Name = "size")]
        public long Size { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "version")]
        public long Version { get; set; }

        [DataMember(Name = "timestamp")]
        [JsonFormatter(typeof(UnixTimestampInt64DateTimeFormatter))]
        public DateTime Timestamp { get; set; }

        [DataMember(Name = "trigger")]
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
            return Encoding.UTF8.GetString(this.Body);
        }
        public void SetBody(string body)
        {
            this.Body = Encoding.UTF8.GetBytes(body);
        }
    }

    public class Trigger
    {
        [DataMember(Name = "class")]
        public string Class { get; set; }

        [DataMember(Name = "kind")]
        public string Kind { get; set; }
    }
}
