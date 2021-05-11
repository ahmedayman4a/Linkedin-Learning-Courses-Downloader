using System;
using System.IO;
using Newtonsoft.Json;

namespace LLCD.DownloaderConfig
{
    internal class DirectoryInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DirectoryInfo);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            try
            {
                return new DirectoryInfo(value);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot unmarshal type DirectoryInfo", ex);
            }


        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DirectoryInfo)untypedValue;
            try
            {
                serializer.Serialize(writer, value.FullName);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot marshal type DirectoryInfo", ex);
            }


        }

        public static readonly DirectoryInfoConverter Singleton = new DirectoryInfoConverter();

    }
}
