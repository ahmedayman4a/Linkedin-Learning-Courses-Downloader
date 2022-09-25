using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LLCD.CourseExtractor
{
    public class ExtractionSession : IDisposable
    {
        public HttpClient Client { get; set; }
        private CookieContainer _cookieContainer;
        private bool _sessionStarted = false;


        public ExtractionSession(string token)
        {
            _cookieContainer = new CookieContainer();
            _cookieContainer.Add(new Cookie("li_at", token, "/", ".www.linkedin.com"));
            var clienthandler = new HttpClientHandler { UseCookies = true, CookieContainer = _cookieContainer };
            Client = new HttpClient(clienthandler);
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
        }

        /// <summary>
        /// Initializes the extraction session by getting and adding all the required headers
        /// like: Csrf-Token and x-li-identity
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if token is invalid</exception>
        public async Task Initialize()
        {
            _sessionStarted = true;
            var response = await GetResponse("https://www.linkedin.com/learning");
            string linkedInHomeRaw = await response.Content.ReadAsStringAsync();
            linkedInHomeRaw = WebUtility.HtmlDecode(linkedInHomeRaw);

            Regex patternTrialLink = new Regex(@"nav__button-tertiary.*\n?.\r?.*Start free trial");
            if (patternTrialLink.IsMatch(linkedInHomeRaw))
            {
                throw new ArgumentException("Provided token is invalid");
            }

            Client.DefaultRequestHeaders.Add("Csrf-Token", ExtractJSessionID());
            string enterpriseProfileHash = ExtractEnterpriseProfileHash();
            if (!String.IsNullOrEmpty(enterpriseProfileHash))
            {
                Client.DefaultRequestHeaders.Add("x-li-identity", enterpriseProfileHash);
            }



            string ExtractJSessionID()
            {
                var cookies = _cookieContainer.GetCookies(new Uri("https://www.linkedin.com/learning"));
                if (cookies is null)
                {
                    throw new Exception("No cookies are found for home page.");
                }
                string jsessionID = cookies["JSESSIONID"].Value;
                if (String.IsNullOrWhiteSpace(jsessionID))
                {
                    throw new Exception("JSESSIONID cookie can't be empty.");
                }
                return jsessionID;
            }

            string ExtractEnterpriseProfileHash()
            {
                Regex patternEnterpriseProfileHash = new Regex(@"enterpriseProfileHash"":""(?<enterpriseProfileHash>.*?)""");

                if (patternEnterpriseProfileHash.IsMatch(linkedInHomeRaw))
                {
                    return patternEnterpriseProfileHash.Match(linkedInHomeRaw).Groups["enterpriseProfileHash"].Value;
                }
                return null;
            }
        }

        /// <summary>
        /// Sends a GET request to the specified Url. If <see cref="Initialize()"/> 
        /// hasn't been called yet, it will be called automatically.
        /// </summary>
        /// <param name="requestUrl">The url the request is sent to</param>
        /// <returns>Task object representing the response</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<HttpResponseMessage> GetResponse(string requestUrl)
        {
            if (!_sessionStarted)
            {
                await Initialize();
            }

            HttpResponseMessage response = await Client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                Log.Error("Failed GET request, returned content : \n" + WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync()));
                throw new HttpRequestException($"Failed GET request. Error {response.StatusCode}. Reason : {response.ReasonPhrase}");
            }
            return response;
        }



        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}
