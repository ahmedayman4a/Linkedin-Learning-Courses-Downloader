using System;
using System.Collections.Generic;
using System.Text;

namespace LLCD.CourseContent
{
    public enum Quality
    {
        High,
        Medium,
        Low
    }

    public static class QualityExtensions
    {
        public static string ToHeight(this Quality q)
        {
            switch (q)
            {
                case Quality.High:
                    return "720";
                case Quality.Medium:
                    return "540";
                case Quality.Low:
                    return "360";
                default:
                    throw new ArgumentException("Undefined Quality");
            }
        }
    }
}
