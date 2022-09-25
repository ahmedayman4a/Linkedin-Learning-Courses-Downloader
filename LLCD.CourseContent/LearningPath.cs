using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLCD.CourseContent
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class LearningPath
    {
        [JsonProperty("elements[0].sections[*].items[*].content.*.slug")]
        public List<string> CoursesSlugs { get; set; }

        public static LearningPath FromJson(string json) => JsonConvert.DeserializeObject<LearningPath>(json, Converter.Settings);

        public override bool Equals(object obj)
        {
            return obj is LearningPath learningPath &&
                   CoursesSlugs.SequenceEqual(learningPath.CoursesSlugs);
        }

        public override int GetHashCode()
        {
            int hashCode = -1331293932;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(CoursesSlugs);
            return hashCode;
        }
    }
}
