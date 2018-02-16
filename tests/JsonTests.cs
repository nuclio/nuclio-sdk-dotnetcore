using Microsoft.VisualStudio.TestTools.UnitTesting;
using nuclio_sdk_dotnetcore;

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

            var serialized = Helpers<Response>.Serialize(response);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
            Assert.AreEqual(serialized, responseString);
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

            var deserialized = Helpers<Response>.Deserialize(responseString);
            Assert.IsNotNull(deserialized);
            Assert.ReferenceEquals(deserialized, response);
        }

        [TestMethod]
        public void SerializeEvent()
        {
            var eventsString = "{\"body\":\"testbody\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":9223372036854775807,\"id\":\"123\",\"method\":\"testmethod\",\"path\":\"testpath\",\"url\":\"http://localhost\",\"version\":1234,\"timestamp\":1518764461,\"trigger\":{\"class\":\"testclass\",\"kind\":\"testkind\"}}";
            
            var eve = new Event();
            eve.Body = "testbody";
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";
            eve.Method = "testmethod";
            eve.Path = "testpath";
            eve.Size = long.MaxValue;
            eve.Timestamp = new System.DateTime(2018, 02, 16, 07, 01, 01);
            eve.Trigger.Class = "testclass";
            eve.Trigger.Kind = "testkind";
            eve.Url = "http://localhost";
            eve.Version = 1234;

            var serialized = Helpers<Event>.Serialize(eve);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
            Assert.AreEqual(serialized, eventsString);

        }

         [TestMethod]
        public void DeserializeEvent()
        {
            var eventsString = "{\"body\":\"testbody\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":9223372036854775807,\"id\":\"123\",\"method\":\"testmethod\",\"path\":\"testpath\",\"url\":\"http://localhost\",\"version\":1234,\"timestamp\":1518764461,\"trigger\":{\"class\":\"testclass\",\"kind\":\"testkind\"}}";
            
            var eve = new Event();
            eve.Body = "testbody";
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";
            eve.Method = "testmethod";
            eve.Path = "testpath";
            eve.Size = long.MaxValue;
            eve.Timestamp = new System.DateTime(2018, 02, 16, 07, 01, 01);
            eve.Trigger.Class = "testclass";
            eve.Trigger.Kind = "testkind";
            eve.Url = "http://localhost";
            eve.Version = 1234;

            var deserialized = Helpers<Event>.Deserialize(eventsString);
            Assert.IsNotNull(deserialized);
            Assert.ReferenceEquals(deserialized, eve);

        }
    }
}
