using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LLCD.CourseContent
{
    public class Chapter
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("videos")]
        public List<Video> Videos { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Chapter chapter &&
                   Videos.SequenceEqual(chapter.Videos) &&
                   Title == chapter.Title;
        }

        public override int GetHashCode()
        {
            int hashCode = 2101407998;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Video>>.Default.GetHashCode(Videos);
            return hashCode;
        }
    }
}
