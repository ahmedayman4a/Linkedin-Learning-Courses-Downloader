using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLCD.CourseExtractor
{
    public static class TokenExtractor
    {
        public static string ExtractToken(Browser browser)
        {
            var cookieExtractor = new CookiesExtractor(".www.linkedin.com");
            List<DBCookie> cookies;
            switch (browser)
            {
                case Browser.Chrome:
                    cookies = cookieExtractor.ReadChromeCookies();
                    break;
                case Browser.Firefox:
                    cookies = cookieExtractor.ReadFirefoxCookies();
                    break;
                case Browser.Edge:
                    cookies = cookieExtractor.ReadEdgeCookies();
                    break;
                default:
                    throw new ArgumentException("browser");
            }
            if (cookies.Where(c => c.Name == "li_at").Count() != 0)
            {
                return cookies.Where(c => c.Name == "li_at").First().Value;
            }
            return null;
        }
    }
}
