using LLCD.DownloaderConfig;
using LLCD.CourseContent;
using LLCD.CourseExtractor;
using Newtonsoft.Json;
using Serilog;
using ShellProgressBar;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LLCD.DownloaderTUI
{
    class Program
    {

        private static readonly ProgressBarOptions optionPbarExtractor = new ProgressBarOptions
        {
            ScrollChildrenIntoView = true,
            ForegroundColor = ConsoleColor.Blue,
            ForegroundColorDone = ConsoleColor.DarkGreen,
            BackgroundColor = ConsoleColor.Gray,
            ProgressBarOnBottom = true
        };
        static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += AllUnhandledExceptions;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("./logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                .CreateLogger();
            Intro();
            Console.Title = "Linkedin Learning Courses Downloader";
            await RunApp();

            Console.WriteLine();
            Console.ReadLine();
        }

        private static async Task RunApp()
        {
            if (File.Exists("./Config.json"))
            {
                Console.WriteLine(TUI.STARTGLYPH + "Found a Config file");
                try
                {
                    Config config = Config.FromJson(File.ReadAllText("./Config.json"));
                    Console.WriteLine(TUI.CONTINUEGLYPH + "Data in config file : ");
                    Console.WriteLine(TUI.CONTINUEGLYPH + "Quality to download in : " + config.Quality);
                    Console.WriteLine(TUI.CONTINUEGLYPH + "Course Directory/Path : " + config.CourseDirectory);
                    Console.WriteLine(TUI.CONTINUEGLYPH + "Authentication Token : " + config.AuthenticationToken);
                    if (TUI.UseConfig())
                    {
                        await RunWithConfig(config);
                    }
                    else
                    {
                        Console.WriteLine(TUI.CONTINUEGLYPH + "The data you enter will be saved in a new Config file");
                        await RunWithoutConfig();
                    }

                }
                catch (JsonSerializationException)
                {
                    TUI.ShowError("Config file is corrupt");
                    Console.WriteLine(TUI.CONTINUEGLYPH + "The data you enter will be saved in a new Config file");
                    await RunWithoutConfig();
                }

            }
            else
            {
                Console.WriteLine(TUI.STARTGLYPH + "Config File not found");
                Console.WriteLine(TUI.CONTINUEGLYPH + "The data you enter will be saved in a new Config file");
                await RunWithoutConfig();

            }
        }

        private static void Intro()
        {
            Console.Write(@"

█░░ █ █▄░█ █▄▀ █▀▀ █▀▄ █ █▄░█   █░░ █▀▀ ▄▀█ █▀█ █▄░█ █ █▄░█ █▀▀
█▄▄ █ █░▀█ █░█ ██▄ █▄▀ █ █░▀█   █▄▄ ██▄ █▀█ █▀▄ █░▀█ █ █░▀█ █▄█

█▀▀ █▀█ █░█ █▀█ █▀ █▀▀ █▀   █▀▄ █▀█ █░█░█ █▄░█ █░░ █▀█ ▄▀█ █▀▄ █▀▀ █▀█
█▄▄ █▄█ █▄█ █▀▄ ▄█ ██▄ ▄█   █▄▀ █▄█ ▀▄▀▄▀ █░▀█ █▄▄ █▄█ █▀█ █▄▀ ██▄ █▀▄


╔═╗╔═╗────╔╗───╔╗──────────╔╗─────────╔╗──────────────╔╗─╔╗
║║╚╝║║────║║───║║──────────║║─────────║║──────────────║║─║║
║╔╗╔╗╠══╦═╝╠══╗║╚═╦╗─╔╦╗╔══╣╚═╦╗╔╦══╦═╝╠══╦╗─╔╦╗╔╦══╦═╣╚═╝╠══╗
║║║║║║╔╗║╔╗║║═╣║╔╗║║─║╠╝║╔╗║╔╗║╚╝║║═╣╔╗║╔╗║║─║║╚╝║╔╗║╔╬╦═╗║╔╗║
║║║║║║╔╗║╚╝║║═╣║╚╝║╚═╝╠╗║╔╗║║║║║║║║═╣╚╝║╔╗║╚═╝║║║║╔╗║║║║─║║╔╗║
╚╝╚╝╚╩╝╚╩══╩══╝╚══╩═╗╔╩╝╚╝╚╩╝╚╩╩╩╩══╩══╩╝╚╩═╗╔╩╩╩╩╝╚╩╝╚╝─╚╩╝╚╝
──────────────────╔═╝║────────────────────╔═╝║
──────────────────╚══╝────────────────────╚══╝

");
        }

        private static async Task RunWithConfig(Config config)
        {
            string courseUrl = TUI.GetCourseUrl();

            await RunExtractorAndDownloader(config, courseUrl);

        }

        private static async Task RunWithoutConfig()
        {
            string courseUrl = TUI.GetCourseUrl();
            string token = TUI.GetLoginToken();
            var courseRootDirectory = TUI.GetPath();
            var selectedQuality = TUI.GetQuality();


            Config config = new Config
            {
                AuthenticationToken = token,
                Quality = selectedQuality,
                CourseDirectory = courseRootDirectory
            };
            await config.Save();
            Console.WriteLine(TUI.CONTINUEGLYPH + "Saved entries to config file");
            Log.Information("Saved entries to config file. Intializing Course Extractor");

            await RunExtractorAndDownloader(config, courseUrl);
        }

        private static async Task RunExtractorAndDownloader(Config config, string courseUrl)
        {
            Console.WriteLine(TUI.CONTINUEGLYPH + "Extracting Course Data. This might take some time...");
            var extractor = new Extractor(courseUrl, config.Quality, config.AuthenticationToken);
            if (!extractor.HasValidUrl())
            {
                TUI.ShowError("The course url you provided is not a recognized valid Linkedin Learning link");
                await RunWithConfig(config);
                return;
            }
            if (!await extractor.HasValidToken())
            {
                TUI.ShowError("The token you provided is not valid");
                await RunWithoutConfig();
                return;
            }
            Course course;
            try
            {
                using var pbarExtractor = new ProgressBar(10000, "Extracting Course Links - This will take some time", optionPbarExtractor);
                course = await extractor.GetCourse(pbarExtractor.AsProgress<float>());
            }
            catch (Exception ex)
            {
                TUI.ShowError(ex.Message);
                Log.Error(ex, ex.Message);
                await RunWithoutConfig();
                return;
            }
            Console.WriteLine(TUI.ENDGLYPH + "Course Extracted Successfully");
            Log.Information("Course Extracted. Downloading...");
            Console.WriteLine();
            CourseDownloader.DownloadCourse(course, config.CourseDirectory);
        }
        private static async void AllUnhandledExceptions(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            Log.Error(ex, "Unknown error occured. Running app again");
            Console.WriteLine();
            TUI.ShowError("Unknown error occured - " + ex.Message);
            TUI.ShowError("Restarting...");
            await RunApp();
        }
    }
}
