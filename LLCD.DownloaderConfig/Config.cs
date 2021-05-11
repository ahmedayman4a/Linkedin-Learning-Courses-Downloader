using LLCD.CourseContent;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LLCD.DownloaderConfig
{
    public class Config
    {
        [JsonProperty("EncryptedAuthenticationToken")]
        private string AuthenticationTokenEncrypted { get; set; }

        [JsonIgnore]
        public string AuthenticationToken
        {
            get
            {
                return DecryptToken(AuthenticationTokenEncrypted);
            }
            set
            {
                AuthenticationTokenEncrypted = EncryptToken(value);
            }
        }
        [JsonProperty("CourseDirectory")]
        public DirectoryInfo CourseDirectory { get; set; }


        [JsonProperty("Quality")]
        public Quality Quality { get; set; }


        private string EncryptToken(string token)
        {
            byte[] b = ASCIIEncoding.ASCII.GetBytes(token);
            string encryptedToken = Convert.ToBase64String(b);
            return encryptedToken;
        }

        public string DecryptToken(string encryptedToken)
        {
            byte[] b;
            string decryptedToken;
            try
            {
                b = Convert.FromBase64String(encryptedToken);
                decryptedToken = ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decryptedToken = "";
            }
            return decryptedToken;
        }

        public async Task Save()
        {
            using (var streamWriter = new StreamWriter("./Config.json", false))
                await streamWriter.WriteAsync(this.ToJson());
        }

        public static async Task<Config> Fill()
        {
            using (var streamReader = new StreamReader("./Config.json"))
                return FromJson(await streamReader.ReadToEndAsync());
        }

        /// <summary>
        /// Make a backup of our config.
        /// Used to persist config across updates.
        /// </summary>
        public static void Backup()
        {
            Log.Information("Backing up config file");
            if (!File.Exists("./Config.json"))
            {
                Log.Information("No config file to backup is found");
                return;
            }
            File.Copy("./Config.json", "../ConfigBackup.json", true);
            Log.Information("Config file backed up successfully");
        }

        /// <summary>
        /// Restore our config backup if any.
        /// Used to persist config across updates.
        /// </summary>
        public static void Restore()
        {
            Log.Information("Restoring config file");
            //Restore settings after application update            
            string destFile = "./Config.json";
            string sourceFile = "../ConfigBackup.json";
            // Check if we have settings that we need to restore
            if (!File.Exists(sourceFile))
            {
                Log.Information("No config file to restore is found");
                return;
            }

            // Copy our backup file in place 
            File.Copy(sourceFile, destFile, true);

            // Delete backup file
            File.Delete(sourceFile);
            Log.Information("Restored config file");

        }

        public static Config FromJson(string json) => JsonConvert.DeserializeObject<Config>(json, Converter.Settings);

    }
    public static class SerializeConfig
    {
        public static string ToJson(this Config self) => JsonConvert.SerializeObject(self, Formatting.Indented, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                QualityConverter.Singleton,
                DirectoryInfoConverter.Singleton
            },
        };
    }

}
