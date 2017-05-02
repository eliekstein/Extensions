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

            Assert.Equal("{\"id\":1,\"name\":\"testing perent\",\"keyTest\":\"5\"}", final);
        }

    }

    class JsonKeyTest
    {
        //[JsonConverter(typeof(KeyOnlyConverter))]
        [Key]
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
