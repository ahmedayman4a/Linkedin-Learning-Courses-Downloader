using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LLCD.CourseContent
{
    public class TranscriptLine
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("transcriptStartAt")]
        public long StartsAt { get; set; }
    }
}
