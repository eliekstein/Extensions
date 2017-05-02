using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Extensions.Test
{
    public static class KeyOnlyConverterTest
    {
        private static string sampleJson = "{\"id\":1,\"name\":\"testing perent\",\"keyTest\":\"5\"}";
        [Fact]
        public static void Should_only_serielize_key()
        {
            var sut = new JsonKeyTestPerent
            {
                id = 1,
                name = "testing perent",
                keyTest = new JsonKeyTest { id = 5, name = "testing child" },
            };

            var final = JsonConvert.SerializeObject(sut);

            Assert.Equal(sampleJson, final);
        }
        [Fact]
        public static void Should_only_deserielize_key()
        {
            var sut = new JsonKeyTestPerent
            {
                id = 1,
                name = "testing perent",
                keyTest = new JsonKeyTest { id = 5 },
            };
          

            var final = JsonConvert.DeserializeObject<JsonKeyTestPerent>(sampleJson);

            Assert.Equal(sut.id, final.id);
            Assert.Equal(sut.keyTest.id, final.keyTest.id);
        }

    }

    class JsonKeyTest
    {
        //[JsonConverter(typeof(KeyOnlyConverter))]
        [Key]
        [JsonProperty("nm")]
        public int id { get; set; }
        public string name { get; set; }

    }
    class JsonKeyTestPerent
    {
        public int id { get; set; }
        public string name { get; set; }
        [JsonConverter(typeof(KeyOnlyConverter))]
        public JsonKeyTest keyTest { get; set; }

    }
}
