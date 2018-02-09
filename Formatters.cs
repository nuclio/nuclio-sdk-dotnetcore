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