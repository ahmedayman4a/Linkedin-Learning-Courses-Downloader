using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLCD.CourseContent
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Course
    {

        [JsonProperty("elements[0].chapters")]
        public List<Chapter> Chapters { get; set; }

        [JsonProperty("elements[0].title")]
        public string Title { get; set; }

        [JsonProperty("elements[0].exerciseFiles")]
        public List<ExerciseFile> ExerciseFiles { get; set; }

        public string Slug { get; set; }

        public static Course FromJson(string json) => JsonConvert.DeserializeObject<Course>(json, Converter.Settings);

        public override bool Equals(object obj)
        {
            bool istrue = EqualityComparer<List<Chapter>>.Default.Equals(Chapters, ((Course)obj).Chapters);
            return obj is Course course &&
                   Chapters.SequenceEqual(course.Chapters) &&
                   Title == course.Title &&
                   Slug == course.Slug;
        }

        public override int GetHashCode()
        {
            int hashCode = -1331293932;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Chapter>>.Default.GetHashCode(Chapters);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Slug);
            return hashCode;
        }
    }
}
