using System;
using LLCD.CourseContent;
using Newtonsoft.Json;

namespace LLCD.DownloaderConfig
{
    internal class QualityConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Quality) || t == typeof(Quality?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "High":
                    return Quality.High;
                case "Medium":
                    return Quality.Medium;
                case "Low":
                    return Quality.Low;
            }
            throw new Exception("Cannot unmarshal type Quality");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Quality)untypedValue;
            switch (value)
            {
                case Quality.High:
                    serializer.Serialize(writer, "High");
                    return;
                case Quality.Medium:
                    serializer.Serialize(writer, "Medium");
                    return;
                case Quality.Low:
                    serializer.Serialize(writer, "Low");
                    return;
            }
            throw new Exception("Cannot marshal type Quality");
        }

        public static readonly QualityConverter Singleton = new QualityConverter();

    }
}
