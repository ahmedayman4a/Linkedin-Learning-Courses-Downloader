using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.IO;
using Serilog;

namespace LLCD.CourseExtractor
{
    class CookiesExtractor
    {
        private readonly string _hostName;
        internal CookiesExtractor(string hostName)
        {
            _hostName = hostName;
        }

        internal List<DBCookie> ReadChromeCookies() => ReadChromiumCookies(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data");

        internal List<DBCookie> ReadEdgeCookies() => ReadChromiumCookies(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Edge\User Data");

        internal List<DBCookie> ReadFirefoxCookies()
        {
            var cookies = new List<DBCookie>();
            string profilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles";
            string defaultProfilePath = Directory.EnumerateDirectories(profilesPath).OrderByDescending(dir => Directory.GetLastWriteTime(dir)).First();
            string dbPath = Path.Combine(defaultProfilePath, "cookies.sqlite");

            var connectionString = "Data Source=" + dbPath + ";pooling=false";

            using (var conn = new SQLiteConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                var prm = cmd.CreateParameter();
                prm.ParameterName = "hostName";
                prm.Value = _hostName;
                cmd.Parameters.Add(prm);

                cmd.CommandText = "SELECT name,value FROM moz_cookies WHERE host = @hostName";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cookies.Add(new DBCookie(reader.GetString(0), reader.GetString(1)));
                    }
                }
                conn.Close();
            }
            return cookies;
        }
        private List<DBCookie> ReadChromiumCookies(string profilePath)
        {
            string encKey = File.ReadAllText(Path.Combine(profilePath, "Local State"));
            encKey = JObject.Parse(encKey)["os_crypt"]["encrypted_key"].ToString();
            var decodedKey = ProtectedData.Unprotect(Convert.FromBase64String(encKey).Skip(5).ToArray(), null, DataProtectionScope.LocalMachine);

            // Big thanks to https://stackoverflow.com/a/60611673/6481581 for answering how Chrome 80 and up changed the way cookies are encrypted.

            List<DBCookie> cookies;
            try
            {
                string dbPath = Path.Combine(profilePath, @"Default\Network\Cookies");
                cookies = GetChromeCookiesFromDB(dbPath, decodedKey);
            }
            catch (SQLiteException ex)
            {
                Log.Error(ex, @"Cookies not found at ""Default\Network\Cookies"". Trying ""Default\Cookies""");
                string dbPath = Path.Combine(profilePath, @"Default\Cookies");
                cookies = GetChromeCookiesFromDB(dbPath, decodedKey);
            }


            return cookies;
        }

        private List<DBCookie> GetChromeCookiesFromDB(string dbPath, byte[] decodedKey)
        {
            var cookies = new List<DBCookie>();
            var connectionString = "Data Source=" + dbPath + ";pooling=false";

            using (var conn = new SQLiteConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                var prm = cmd.CreateParameter();
                prm.ParameterName = "hostName";
                prm.Value = _hostName;
                cmd.Parameters.Add(prm);

                cmd.CommandText = "SELECT name,encrypted_value FROM cookies WHERE host_key = @hostName";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var encryptedData = (byte[])reader[1];
                        var decodedValue = DecryptWithKey(encryptedData, decodedKey, 3);
                        cookies.Add(new DBCookie(reader.GetString(0), decodedValue));
                    }
                }
                conn.Close();
            }
            return cookies;
        }

        private string DecryptWithKey(byte[] message, byte[] key, int nonSecretPayloadLength)
        {
            const int KEY_BIT_SIZE = 256;
            const int MAC_BIT_SIZE = 128;
            const int NONCE_BIT_SIZE = 96;

            if (key == null || key.Length != KEY_BIT_SIZE / 8)
                throw new ArgumentException(String.Format("Key needs to be {0} bit!", KEY_BIT_SIZE), "key");
            if (message == null || message.Length == 0)
                throw new ArgumentException("Message required!", "message");

            using (var cipherStream = new MemoryStream(message))
            using (var cipherReader = new BinaryReader(cipherStream))
            {
                var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);
                var nonce = cipherReader.ReadBytes(NONCE_BIT_SIZE / 8);
                var cipher = new GcmBlockCipher(new AesEngine());
                var parameters = new AeadParameters(new KeyParameter(key), MAC_BIT_SIZE, nonce);
                cipher.Init(false, parameters);
                var cipherText = cipherReader.ReadBytes(message.Length);
                var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];
                try
                {
                    var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                    cipher.DoFinal(plainText, len);
                }
                catch (InvalidCipherTextException)
                {
                    return null;
                }
                return Encoding.Default.GetString(plainText);
            }
        }
    }
}
