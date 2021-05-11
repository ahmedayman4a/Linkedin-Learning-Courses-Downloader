using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLCD.CourseContent
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Video
    {
        private List<TranscriptLine> _transcriptLines;

        [JsonProperty("elements[0].selectedVideo.title")]
        public string Title { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "elements[0].selectedVideo.durationInSeconds", Order = -2)]
        public int Duration { get; set; }

        [JsonProperty("elements[0].selectedVideo.url.progressiveUrl")]
        public string DownloadUrl { get; set; }

        [JsonProperty("elements[0].selectedVideo.transcript.lines")]
        public List<TranscriptLine> TranscriptLines
        {
            get => _transcriptLines;
            set
            {
                _transcriptLines = value;
                FormTranscript();
            }
        }

        public string Transcript { get; set; }

        public static Video FromJson(string json) => JsonConvert.DeserializeObject<Video>(json, Converter.Settings);

        public override bool Equals(object obj)
        {
            return obj is Video video &&
                   Title == video.Title &&
                   Slug == video.Slug &&
                   Duration == video.Duration &&
                   Transcript == video.Transcript;
        }

        public override int GetHashCode()
        {
            int hashCode = -1567037767;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Slug);
            hashCode = hashCode * -1521134295 + Duration.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Transcript);
            return hashCode;
        }

        private void FormTranscript()
        {
            if (_transcriptLines != null)
            {
                Transcript = "";
                for (int i = 0; i < _transcriptLines.Count; i++)
                {
                    string startsAt = TimeSpan.FromMilliseconds(_transcriptLines[i].StartsAt).ToString(@"hh\:mm\:ss\,fff");
                    long endsAtMS = i + 1 == _transcriptLines.Count ? Duration * 1000 : _transcriptLines[i + 1].StartsAt;  //at last line endsAt equals the durations
                    string endsAt = TimeSpan.FromMilliseconds(endsAtMS).ToString(@"hh\:mm\:ss\,fff");
                    Transcript += i + 1 + Environment.NewLine;
                    Transcript += startsAt + " --> " + endsAt + Environment.NewLine;
                    Transcript += _transcriptLines[i].Caption + Environment.NewLine + Environment.NewLine;
                }
                Transcript.Trim();
            }
        }
    }
}
