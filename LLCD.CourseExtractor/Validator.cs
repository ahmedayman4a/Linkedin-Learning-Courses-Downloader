using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LLCD.CourseExtractor
{
    public static class Validator
    {
        private static HttpClient _client;
        private static CookieContainer _cookieContainer;

        static Validator()
        {
            _cookieContainer = new CookieContainer();
            var clienthandler = new HttpClientHandler { UseCookies = true, CookieContainer = _cookieContainer };
            _client = new HttpClient(clienthandler);
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
        }


        public static async Task<bool> IsTokenValid(string token)
        {
            _cookieContainer.Add(new Cookie("li_at", token, "/", ".www.linkedin.com"));
            Regex patternTrialLink = new Regex(@"nav__button-tertiary.*\n?.\r?.*Start free trial");
            var response = await _client.GetAsync("https://www.linkedin.com/learning");
            string linkedInHomeRaw = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Log.Error("Home page GET request content : \n" + linkedInHomeRaw);
                throw new Exception("Failed to get home page. Error " + response.StatusCode);
            }
            linkedInHomeRaw = WebUtility.HtmlDecode(linkedInHomeRaw);

            if (patternTrialLink.IsMatch(linkedInHomeRaw))
            {
                return false;
            }

            return true;
        }

        public static bool AreUrlsValid(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                return IsUrlValid(url);
            }
            return true;
        }

        public static bool IsUrlValid(string url)
        {
            if (UrlTypeHelper.CategorizeUrl(url) == UrlType.Invalid)
            {
                return false;
            }
            return true;
        }
    }
}
