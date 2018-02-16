using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace nuclio_sdk_dotnetcore
{
    public class DateTimeUnixFormatter : IMessagePackFormatter<DateTime>
    {
        public DateTime Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var unixTimestamp = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            var unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            unixDateTime = unixDateTime.AddSeconds(unixTimestamp).ToUniversalTime();
            return unixDateTime;
        }

        public int Serialize(ref byte[] bytes, int offset, DateTime value, IFormatterResolver formatterResolver)
        {
            var unixTimestamp = (Int64)(value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))).TotalSeconds;
            return MessagePackBinary.WriteInt64(ref bytes, offset, unixTimestamp);
        }
    }

    public class Base64StringFormatter : IMessagePackFormatter<String>
    {
        public String Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {

            var encodedString = MessagePackBinary.ReadString(bytes, offset, out readSize);
            return Encoding.ASCII.GetString(System.Convert.FromBase64String(encodedString));
        }

        public int Serialize(ref byte[] bytes, int offset, String value, IFormatterResolver formatterResolver)
        {
            var byteArr = Encoding.ASCII.GetBytes(value);
            return MessagePackBinary.WriteString(ref bytes, offset, System.Convert.ToBase64String(byteArr));
        }
    }
}