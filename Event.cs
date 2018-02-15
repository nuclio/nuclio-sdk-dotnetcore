﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace nuclio_sdk_dotnetcore
{
    [MessagePackObject]
    public class Event
    {
        [Key("body")]
        [MessagePackFormatter(typeof(ByteStringFormatter))]
        public string Body { get; set; }

        [Key("content-type")]
        public string ContentType { get; set; }

        [Key("headers")]
        public Dictionary<string, object> Headers { get; set; }

        [Key("fields")]
        public Dictionary<string, object> Fields { get; set; }

        [Key("size")]
        public long Size { get; set; }

        [Key("id")]
        public string Id { get; set; }

        [Key("method")]
        public string Method { get; set; }

        [Key("path")]
        public string Path { get; set; }

        [Key("url")]
        public string Url { get; set; }

        [Key("version")]
        public long Version { get; set; }

        [Key("timestamp")]
        [MessagePackFormatter(typeof(DateTimeUnixFormatter))]
        public DateTime Timestamp { get; set; }

        [Key("trigger")]
        public Trigger Trigger { get; set; }

        public static Event Deserialize(string eventString)
        {    
            var bin = MessagePackSerializer.FromJson(eventString);
            return MessagePackSerializer.Deserialize<Event>(bin);
        }

        public string Serialize()
        {
            var bin = MessagePackSerializer.Serialize(this);
            return MessagePackSerializer.ToJson(bin);
        }
    }

    [MessagePackObject]
    public class Trigger
    {
        [Key("class")]
        public string Class { get; set; }

        [Key("kind")]
        public string Kind { get; set; }
    }
}