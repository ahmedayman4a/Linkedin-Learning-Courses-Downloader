using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLCD.CourseContent
{
    public class ExerciseFile
    {
        [JsonProperty("name")]
        public string FileName { get; set; }

        [JsonProperty("url")]
        public string DownloadUrl { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ExerciseFile file &&
                   FileName == file.FileName;
        }

        public override int GetHashCode()
        {
            int hashCode = 1274101074;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FileName);
            return hashCode;
        }
    }
}
