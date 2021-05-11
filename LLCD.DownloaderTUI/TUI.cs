using LLCD.CourseContent;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LLCD.DownloaderTUI
{
    public static class TUI
    {
        public const string ANSWERGLYPH = "╠════════";
        public const string STARTGLYPH = "╔══";
        public const string CONTINUEGLYPH = "╠══";
        public const string ENDGLYPH = "╚══";

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(CONTINUEGLYPH + "[!!] " + message);
            Console.ResetColor();
        }

        public static bool UseConfig()
        {
            while (true)
            {
                Console.WriteLine(CONTINUEGLYPH + "Would you like to use this configuration?");
                Console.WriteLine(CONTINUEGLYPH + "1.Yes 2.No");
                Console.Write(ANSWERGLYPH);
                string answer = Console.ReadLine();
                switch (answer.Clean())
                {
                    case "yes":
                    case "y":
                    case "1.yes":
                    case "1":
                        return true;
                    case "no":
                    case "n":
                    case "2.n":
                    case "2":
                        return false;
                    default:
                        ShowError("The answer you entered isn't recognized");
                        ShowError("Please try again");
                        break;
                }
            }
        }
        public static DirectoryInfo GetPath()
        {
            while (true)
            {
                Console.WriteLine(CONTINUEGLYPH + "Where do you want to download your course to?(ex:D:\\MyCourses)");
                Console.Write(ANSWERGLYPH);
                string pathToCourse = Console.ReadLine().Clean(false);
                if (!Directory.Exists(pathToCourse))
                {
                    ShowError("Provided directory doesn't exist");
                }
                else
                {
                    return new DirectoryInfo(pathToCourse);
                }
            }
        }

        public static string GetLoginToken()
        {

            string loginToken = "";
            while (string.IsNullOrEmpty(loginToken))
            {
                Console.WriteLine(CONTINUEGLYPH + "What is the linkedin learning security token?(It should appear as li_at if you are loged into linkedin learning)");
                Console.Write(ANSWERGLYPH);
                loginToken = Console.ReadLine().Clean(false);
            }
            return loginToken;
        }

        public static string GetCourseUrl()
        {
            while (true)
            {
                Console.WriteLine(CONTINUEGLYPH + "What is the url of the course?");
                Console.Write(ANSWERGLYPH);
                return Console.ReadLine().Clean();
                
            }
        }

        public static Quality GetQuality()
        {
            while (true)
            {
                Console.WriteLine(CONTINUEGLYPH + "Which quality would you like the course to be downloaded in?");
                Console.WriteLine(CONTINUEGLYPH + "Available Qualities : 1.360p 2.540p 3.720p");
                Console.Write(ANSWERGLYPH);
                string quality = Console.ReadLine();
                switch (quality.Clean())
                {

                    case "1":
                    case "360":
                    case "360p":
                        return Quality.Low;
                    case "2":
                    case "540":
                    case "540p":
                        return Quality.Medium;
                    case "3":
                    case "720":
                    case "720p":
                        return Quality.High;
                    default:
                        ShowError("The quality you entered isn't recognized");
                        ShowError("Please try again");
                        break;
                }
            }
        }

        private static string Clean(this string answer, bool toLower = true)
        {
            if (toLower)
            {
                return answer.ToLower().Replace(ANSWERGLYPH, "").Trim();
            }
            else
            {
                return answer.Replace(ANSWERGLYPH, "").Trim();
            }
        }

    }
}
