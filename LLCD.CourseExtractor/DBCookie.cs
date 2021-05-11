
namespace LLCD.CourseExtractor
{
    class DBCookie
    {
        internal DBCookie() { }
        internal DBCookie(string name, string value)
        {
            Name = name;
            Value = value;
        }
        internal string Name { get; set; }
        internal string Value { get; set; }
    }
}
