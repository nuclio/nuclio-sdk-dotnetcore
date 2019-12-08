using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuclio.Sdk;

namespace tests
{
    [TestClass]
    public class JsonTests
    {

        [TestMethod]
        public void SerializeResponse()
        {
            var responseString = "{\"body\":\"test\",\"content_type\":\"plain/text\",\"status_code\":200,\"headers\":{\"testkey\":\"testvalue\"},\"body_encoding\":\"text\"}";

            var response = new Response();
            response.Body = "test";
            response.BodyEncoding = "text";
            response.ContentType = "plain/text";
            response.StatusCode = 200;
            response.Headers.Add("testkey", "testvalue");

            var serialized = NuclioSerializationHelpers<Response>.Serialize(response);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
            serialized.Should().BeEquivalentTo(responseString);
        }

        [TestMethod]
        public void DeserializeResponse()
        {
            var responseString = "{\"body\":\"test\",\"content_type\":\"plain/text\",\"status_code\":200,\"headers\":{\"testkey\":\"testvalue\"},\"body_encoding\":\"text\"}";

            var response = new Response();
            response.Body = "test";
            response.BodyEncoding = "text";
            response.ContentType = "plain/text";
            response.StatusCode = 200;
            response.Headers.Add("testkey", "testvalue");

            var deserialized = NuclioSerializationHelpers<Response>.Deserialize(responseString);
            Assert.IsNotNull(deserialized);
            deserialized.Should().BeEquivalentTo(response);
        }

        [TestMethod]
        public void SerializeEvent()
        {
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":9223372036854775807,\"id\":\"123\",\"method\":\"testmethod\",\"path\":\"testpath\",\"url\":\"http://localhost\",\"version\":\"1234\",\"type\":null,\"typeVersion\":null,\"timestamp\":1518771661,\"trigger\":{\"class\":\"testclass\",\"kind\":\"testkind\"}}";
            var eve = new Event();
            eve.SetBody("{\"key1\":\"value1\", \"key2\":\"value2\"}");
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";
            eve.Method = "testmethod";
            eve.Path = "testpath";
            eve.Size = long.MaxValue;
            eve.Timestamp = new System.DateTime(2018, 02, 16, 09, 01, 01, System.DateTimeKind.Utc);
            eve.Trigger.Class = "testclass";
            eve.Trigger.Kind = "testkind";
            eve.Url = "http://localhost";
            eve.Version = "1234";

            var serialized = NuclioSerializationHelpers<Event>.Serialize(eve);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
            serialized.Should().BeEquivalentTo(eventsString);
        }

        [TestMethod]
        public void DeserializeEvent()
        {
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":9223372036854775807,\"id\":\"123\",\"method\":\"testmethod\",\"path\":\"testpath\",\"url\":\"http://localhost\",\"version\":\"1234\",\"timestamp\":1518771661,\"trigger\":{\"class\":\"testclass\",\"kind\":\"testkind\"}}";
            var bodyValue = "{\"key1\":\"value1\", \"key2\":\"value2\"}";
            var eve = new Event();
            eve.SetBody(bodyValue);
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";
            eve.Method = "testmethod";
            eve.Path = "testpath";
            eve.Size = long.MaxValue;
            eve.Timestamp = new System.DateTime(2018, 02, 16, 09, 01, 01, System.DateTimeKind.Utc);
            eve.Trigger.Class = "testclass";
            eve.Trigger.Kind = "testkind";
            eve.Url = "http://localhost";
            eve.Version = "1234";

            var deserialized = NuclioSerializationHelpers<Event>.Deserialize(eventsString);
            Assert.IsNotNull(deserialized);
            deserialized.GetBody().Should().BeEquivalentTo(bodyValue);
            deserialized.Should().BeEquivalentTo(eve);
        }
         [TestMethod]
        public void SerializeMissingPropertiesEvent()
        {
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":0,\"id\":\"123\",\"method\":null,\"path\":null,\"url\":null,\"version\":\"0\",\"type\":null,\"typeVersion\":null,\"timestamp\":-62135596800,\"trigger\":{\"class\":null,\"kind\":null}}";

            var eve = new Event();
            eve.SetBody("{\"key1\":\"value1\", \"key2\":\"value2\"}");
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";
            eve.Version = "0";

            var serialized = NuclioSerializationHelpers<Event>.Serialize(eve);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
            serialized.Should().BeEquivalentTo(eventsString);
        }

        [TestMethod]
        public void DeserializeMissingPropertiesEvent()
        {
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":0,\"id\":\"123\",\"method\":null,\"path\":null,\"url\":null,\"version\":\"0\",\"timestamp\":-62135596800,\"trigger\":{\"class\":null,\"kind\":null}}";
            var bodyValue = "{\"key1\":\"value1\", \"key2\":\"value2\"}";
            var eve = new Event();
            eve.SetBody(bodyValue);
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";
            eve.Version = "0";
            
            var deserialized = NuclioSerializationHelpers<Event>.Deserialize(eventsString);
            Assert.IsNotNull(deserialized);
            deserialized.GetBody().Should().BeEquivalentTo(bodyValue);
            deserialized.Should().BeEquivalentTo(eve);
        }
    }
}
