using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LLCD.CourseContent
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                JsonPathConverter.Singleton
            }
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            int i;
            if (Int32.TryParse(value, out i))
            {
                return i;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (int)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }
    }
    //https://stackoverflow.com/questions/33088462/can-i-specify-a-path-in-an-attribute-to-map-a-property-in-my-class-to-a-child-pr
    internal class JsonPathConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);

            foreach (PropertyInfo prop in objectType.GetProperties()
                                                    .Where(p => p.CanRead && p.CanWrite))
            {
                JsonPropertyAttribute att = prop.GetCustomAttributes(true)
                                                .OfType<JsonPropertyAttribute>()
                                                .FirstOrDefault();

                string jsonPath = (att != null ? att.PropertyName : prop.Name);
                if (jsonPath.Contains("*") && typeof(IList).IsAssignableFrom(prop.PropertyType))
                {
                    var tokens = jo.SelectTokens(jsonPath);
                    var values = (IList)Activator.CreateInstance(prop.PropertyType);
                    foreach (var token in tokens)
                    {
                        if (token != null && token.Type != JTokenType.Null)
                        {
                            object value = token.ToObject(Helpers.GetCollectionElementType(prop.PropertyType), serializer);
                            values.Add(value);
                        }
                    }
                    prop.SetValue(targetObj, values, null);
                }
                else
                {
                    JToken token = jo.SelectToken(jsonPath);

                    if (token != null && token.Type != JTokenType.Null)
                    {
                        object value = token.ToObject(Helpers.GetCollectionElementType(prop.PropertyType), serializer);
                        prop.SetValue(targetObj, value, null);
                    }
                }
            }

            return targetObj;
        }

        public override bool CanConvert(Type objectType) => false;  // CanConvert is not called when [JsonConverter] attribute is used

        public override bool CanWrite { get => false; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();

        public static readonly JsonPathConverter Singleton = new JsonPathConverter();

    }
}
