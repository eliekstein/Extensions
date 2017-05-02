using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Converters
{
    public class KeyOnlyConverter : JsonConverter
    {
        private Predicate<PropertyInfo> propertyInfoFilter =>
            p => p.CustomAttributes
            .Where(a => a.AttributeType == typeof(KeyAttribute)).Count() == 1;

        public override bool CanConvert(Type objectType) =>
            objectType.GetProperties().Any(p => propertyInfoFilter(p));

        public override bool CanRead => false;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var idProperty = value.GetType().GetProperties()
                .Single(p => propertyInfoFilter(p));

            JToken t = JToken.FromObject(idProperty.GetValue(value));

            if (t.Type == JTokenType.Array)
            {
                var col = (IEnumerable<object>)value;
                var strCol = col.Select(c => c.ToString());

                writer.WriteValue(string.Join(",", strCol));
            }
            else
            {
                writer.WriteValue(idProperty.GetValue(value).ToString());
            }

        }
    }
}