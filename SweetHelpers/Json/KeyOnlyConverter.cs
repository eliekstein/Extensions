using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Converters
{
    public class KeyOnlyConverter : JsonConverter// where T : new()
    {
        private Predicate<PropertyInfo> propertyInfoFilter =>
            p => p.CustomAttributes
            .Where(a => a.AttributeType == typeof(KeyAttribute)).Count() == 1;

        protected object Create(Type objectType, JsonReader reader)
        {
            //activate the object with reflection
            var obj = Activator.CreateInstance(objectType);
            //get the object property to be set
            var prop = obj.GetType().GetProperties()
                .Where(p => propertyInfoFilter(p))
                .Single();

            //get json property name
            var name = prop.CustomAttributes
                .SingleOrDefault(p => p.AttributeType == typeof(JsonPropertyAttribute));


            //get json value from jsonObject and convert it to appropiate type
            var jvalue = Convert.ChangeType(reader.Value, prop.PropertyType);

            //set the property value
            prop.SetValue(obj, jvalue);
            return obj;
        }

        public override bool CanConvert(Type objectType) =>
            objectType.GetProperties().Any(p => propertyInfoFilter(p));

        public override bool CanRead => true;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)// =>
                                                                                                                            // throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        {
            var target = Create(objectType, reader);
            return target;
        }

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