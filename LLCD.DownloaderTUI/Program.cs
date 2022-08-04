using LLCD.DownloaderConfig;
using LLCD.CourseContent;
using LLCD.CourseExtractor;
using Newtonsoft.Json;
using Serilog;
using ShellProgressBar;
using System;
using System.IO;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LLCD.DownloaderTUI
{
    class Program
    {
        [Argument(0, Description = "The urls to extract and download, seperated by space.")]
        public string[] Urls { get; set; }

        [Option(Description = "Linkedin learning security token that has access to the provided courses. It should appear as 'li_at' if you are logged into Linkedin Learning.")]
        public string Token { get; set; }

        [Option(Description = "The root directory that the courses will be downloaded to (ex:D:\\MyCourses).")]
        public string DownloadLocation { get; set; }

        [Option("-q|--quality <QUALITY>", Description = "Video download quality.")]
        public Quality? DownloadQuality { get; set; } = null;

        [Option("--no-subtitles", Description = "Do not download subtitle files for videos.")]
        public bool NoSubtitles { get; set; } = false;

        [Option("--no-exercise-files", Description = "Do not download exercise files.")]
        public bool NoExerciseFiles { get; set; } = false;

        [Option("--ignore-config", Description = "Ignores default values present in config file.")]
        public bool IgnoreConfig { get; set; } = false;

        [Option("--no-saving", Description = "Do not save token,DownloadLocation and DownloadQuality to config file")]
        public bool NoSaving { get; set; } = false;

        [Option(Description = "Path to file containing course URLs to download, one URL per line. Either <urls> argument or --batch-file option can be set.")]
        public string BatchFile { get; set; }

        [Option("--delay", Description = "Time in seconds that a random number around it will be used to wait after each video extraction. Max value: 120.")]
        [Range(0, 120)]
        public int Delay { get; set; }

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

            await CommandLineApplication.ExecuteAsync<Program>(args);


        }

        private async Task OnExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
        {
            Intro();
            Console.Title = "Linkedin Learning Courses Downloader";
            if (!IgnoreConfig)
            {
                CheckConfig();
            }

            await PopulateFields();

            bool isConfigSaved = false;
            List<Course> courses = new();
            foreach (var courseUrl in Urls)
            {
                var extractor = new Extractor(courseUrl, (Quality)DownloadQuality, Token, Delay);
                await ValidateFields(extractor);
                if (!isConfigSaved)
                {
                    await SaveConfig();
                    isConfigSaved = true;
                }

                try
                {
                    var course = await GetCourse(courseUrl);
                    courses.Add(course);
                    Console.WriteLine($"{TUI.CONTINUEGLYPH}Course with name [{course.Title}] extracted successfully");
                    Log.Information($"Course {course.Title} extracted successfully");
                }
                catch (Exception ex)
                {
                    TUI.ShowError("Error occured while extracting course at " + courseUrl);
                    TUI.ShowError("Error message : " + ex.Message);
                    Log.Error(ex, ex.Message);
                }

            }


            Console.WriteLine(TUI.ENDGLYPH + "Press enter to exit");
            Console.ReadLine();


            void Intro()
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
            void CheckConfig()
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
                            Token = config.AuthenticationToken;
                            DownloadLocation = config.CourseDirectory.FullName;
                            DownloadQuality = config.Quality;
                        }
                        else
                        {
                            Console.WriteLine(TUI.CONTINUEGLYPH + "The data you enter will be saved in a new Config file");
                        }

                    }
                    catch (JsonSerializationException)
                    {
                        TUI.ShowError("Config file is corrupt");
                        Console.WriteLine(TUI.CONTINUEGLYPH + "The data you enter will be saved in a new Config file");
                    }

                }
                else
                {
                    Console.WriteLine(TUI.STARTGLYPH + "Config File not found");
                    Console.WriteLine(TUI.CONTINUEGLYPH + "The data you enter will be saved in a new Config file");
                }
            }

            async Task SaveConfig()
            {
                if (!NoSaving) // if the --no-saving command line option is not set, save entries to config file
                {
                    Config config = new()
                    {
                        AuthenticationToken = Token,
                        Quality = (Quality)DownloadQuality,
                        CourseDirectory = new DirectoryInfo(DownloadLocation)
                    };
                    await config.Save();
                    Console.WriteLine(TUI.CONTINUEGLYPH + "Saved entries to config file");
                    Log.Information("Saved entries to config file. Intializing Course Extractor");
                }
            }

            async Task PopulateFields()
            {
                if (!String.IsNullOrWhiteSpace(BatchFile))
                {
                    if (!File.Exists(BatchFile))
                    {
                        TUI.ShowError($"BatchFile {BatchFile} doesn't exist or can't be accessed.");
                    }
                    else if (Path.GetExtension(BatchFile) != "txt")
                    {
                        TUI.ShowError($"BatchFile {BatchFile} isn't a txt file.");
                    }
                    else
                    {
                        Urls = await File.ReadAllLinesAsync(BatchFile, cancellationToken);
                    }
                }
                if (Urls?.Length == 0)
                {
                    string url = TUI.GetCourseUrl();
                    Urls = new string[] { url };
                }

                if (String.IsNullOrWhiteSpace(Token))
                {
                    Token = TUI.GetLoginToken();
                }
                if (String.IsNullOrWhiteSpace(DownloadLocation))
                {
                    DownloadLocation = TUI.GetPath().FullName;
                }
                DownloadQuality ??= TUI.GetQuality();
            }

            async Task ValidateFields(Extractor extractor)
            {
                ValidateUrls();
                await ValidateToken();

                void ValidateUrls()
                {
                    if (!extractor.HasValidUrl())
                    {
                        TUI.ShowError("The course url you provided is not a recognized valid Linkedin Learning link");
                        string url = TUI.GetCourseUrl();
                        Urls = new string[] { url };
                        ValidateUrls();
                    }
                }

                async Task ValidateToken()
                {
                    if (!await extractor.HasValidToken())
                    {
                        TUI.ShowError("The token you provided is not valid");
                        Token = TUI.GetLoginToken();
                        await ValidateToken();
                    }
                }
            }

            async Task<Course> GetCourse(string courseUrl)
            {
                Console.WriteLine($"{TUI.CONTINUEGLYPH}Extracting Course at {courseUrl}.");
                Console.WriteLine($"{TUI.CONTINUEGLYPH}This might take some time...");
                var extractor = new Extractor(courseUrl, (Quality)DownloadQuality, Token);

                using var pbarExtractor = new ProgressBar(10000, "Extracting course data at " + courseUrl, optionPbarExtractor);
                Log.Information("Extracting course data at " + courseUrl);
                return await extractor.GetCourse(pbarExtractor.AsProgress<float>());

            }
        }



        private static void AllUnhandledExceptions(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            Log.Error(ex, "Unknown error occured. Running app again");
            Console.WriteLine();
            TUI.ShowError("Unknown error occured - " + ex.Message);
            Console.ReadLine();
        }
    }
}
