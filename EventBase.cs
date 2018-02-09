using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace nuclio_sdk_dotnetcore
{
    [MessagePackObject]
    public abstract class EventBase
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

    }

    [MessagePackObject]
    public class Trigger
    {
        [Key("class")]
        public string Class { get; set; }

        [Key("kind")]
        public string Kind { get; set; }
    }

    public class DateTimeUnixFormatter : IMessagePackFormatter<DateTime>
    {
        public DateTime Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var unixTimestamp = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            var unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            unixDateTime = unixDateTime.AddSeconds(unixTimestamp).ToLocalTime();
            return unixDateTime;
        }

        public int Serialize(ref byte[] bytes, int offset, DateTime value, IFormatterResolver formatterResolver)
        {
            var unixTimestamp = (Int32)(value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return MessagePackBinary.WriteInt32(ref bytes, offset, unixTimestamp);
        }
    }

    public class ByteStringFormatter : IMessagePackFormatter<String>
    {
        public String Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var byteArr = MessagePackBinary.ReadBytes(bytes, offset, out readSize);
            return Encoding.Default.GetString(byteArr);
        }

        public int Serialize(ref byte[] bytes, int offset, String value, IFormatterResolver formatterResolver)
        {
            var byteArr = Encoding.Default.GetBytes(value);
            return MessagePackBinary.WriteBytes(ref bytes, offset, byteArr);
        }
    }
}
