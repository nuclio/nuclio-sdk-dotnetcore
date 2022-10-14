/*
Copyright 2018 The Nuclio Authors.
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
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
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":9223372036854775807,\"id\":\"123\",\"method\":\"testmethod\",\"path\":\"testpath\",\"url\":\"http://localhost\",\"version\":\"1234\",\"type\":\"snowman\",\"typeVersion\":\"0.1.2\",\"timestamp\":1518771661,\"trigger\":{\"class\":\"testclass\",\"kind\":\"testkind\"}}";
            
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
            eve.Type = "snowman";
            eve.TypeVersion = "0.1.2";

            var serialized = NuclioSerializationHelpers<Event>.Serialize(eve);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
            serialized.Should().BeEquivalentTo(eventsString);
        }

        [TestMethod]
        public void DeserializeEvent()
        {
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":9223372036854775807,\"id\":\"123\",\"method\":\"testmethod\",\"path\":\"testpath\",\"url\":\"http://localhost\",\"version\":\"1234\",\"type\":\"snowman\",\"typeVersion\":\"0.1.2\",\"timestamp\":1518771661,\"trigger\":{\"class\":\"testclass\",\"kind\":\"testkind\"}}";
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
            eve.Type = "snowman";
            eve.TypeVersion = "0.1.2";
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
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":0,\"id\":\"123\",\"method\":null,\"path\":null,\"url\":null,\"version\":null,\"type\":null,\"typeVersion\":null,\"timestamp\":-62135578800,\"trigger\":{\"class\":null,\"kind\":null}}";

            var eve = new Event();
            eve.SetBody("{\"key1\":\"value1\", \"key2\":\"value2\"}");
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";

            var serialized = NuclioSerializationHelpers<Event>.Serialize(eve);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
            serialized.Should().BeEquivalentTo(eventsString);
        }

        [TestMethod]
        public void DeserializeMissingPropertiesEvent()
        {
            var eventsString = "{\"body\":\"eyJrZXkxIjoidmFsdWUxIiwgImtleTIiOiJ2YWx1ZTIifQ==\",\"content-type\":\"plain/text\",\"headers\":{\"testkey\":\"testvalue\"},\"fields\":{\"testkey\":\"testvalue\"},\"size\":0,\"id\":\"123\",\"method\":null,\"path\":null,\"url\":null,\"version\":null,\"timestamp\":-62135596800,\"trigger\":{\"class\":null,\"kind\":null}}";
            var bodyValue = "{\"key1\":\"value1\", \"key2\":\"value2\"}";
            var eve = new Event();
            eve.SetBody(bodyValue);
            eve.ContentType = "plain/text";
            eve.Headers.Add("testkey", "testvalue");
            eve.Fields.Add("testkey", "testvalue");
            eve.Id = "123";
            
            var deserialized = NuclioSerializationHelpers<Event>.Deserialize(eventsString);
            Assert.IsNotNull(deserialized);
            deserialized.GetBody().Should().BeEquivalentTo(bodyValue);
            deserialized.Should().BeEquivalentTo(eve);
        }
    }
}
