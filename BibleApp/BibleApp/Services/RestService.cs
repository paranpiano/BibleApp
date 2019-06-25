using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BibleApp.DatabaseAccess;
using BibleApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace BibleApp.Services
{
    public class RestService : IRestService<BibleVerse>
    {
        HttpClient _client;
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://getbible.net" : "https://getbible.net";
        public static string BibleDataUrl = BaseAddress + "/index.php?option=com_getbible&view=json&p={0}&v={1}";


        public RestService()
        {
            _client = new HttpClient();
        }


        public async Task<BibleVerse> CreateBibleVerseData(string id)
        {
            string chapterName = id.Split('_')[2];
            string version = id.Split('_')[0];

            _client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            _client.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");

            try
            {
                string address = string.Format(BibleDataUrl, chapterName, version);

                var responseStream = await _client.GetStreamAsync(address);
                using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                using (var streamReader = new StreamReader(decompressedStream))
                {
                    var result = await streamReader.ReadToEndAsync().ConfigureAwait(false);

                    //remove ( , ); from the string
                    string jsonResult = result.ToString().Remove(result.Length - 2, 2).Remove(0, 1);

                    JObject rss = JObject.Parse(jsonResult);
                    var book = rss["book"].ToObject<Dictionary<string, object>>();
                    List<string> chaterKeys = new List<string>(book.Keys);

                    // Chapter # loop
                    for (int i = 0; i < chaterKeys.Count; i++)
                    {
                        var verserKeys = ((JObject)book[(i + 1).ToString()])["chapter"].
                            ToObject<Dictionary<string, object>>();

                        // Verse # loop
                        var verses = "";
                        for (int j = 0; j < verserKeys.Count; j++)
                        {
                            verses += (j + 1) + ":";
                            verses += ((JObject)verserKeys[(j + 1).ToString()])["verse"] + "\n";
                        }

                        return new BibleVerse()
                        {
                            Version = version,
                            ChapterNr = i + 1,
                            Chapter = chapterName,
                            Verses = verses
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public async Task<BibleVerse> CreateBibleVerseDataAndroid(string id)
        {
            string chapterName = id.Split('_')[2];
            string version = id.Split('_')[0];

            _client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            _client.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");

            try
            {
                version = "aov";
                string address = string.Format(BibleDataUrl, chapterName, version);
                var result = await _client.GetStringAsync(address);

                //remove ( , ); from the string
                string jsonResult = result.ToString().Remove(result.ToString().Length - 2, 2).Remove(0, 1);

                JObject rss = JObject.Parse(jsonResult);
                var book = rss["book"].ToObject<Dictionary<string, object>>();
                List<string> chaterKeys = new List<string>(book.Keys);

                // Chapter # loop
                for (int i = 0; i < chaterKeys.Count; i++)
                {
                    var verserKeys = ((JObject)book[(i + 1).ToString()])["chapter"].
                        ToObject<Dictionary<string, object>>();

                    // Verse # loop
                    var verses = "";
                    for (int j = 0; j < verserKeys.Count; j++)
                    {
                        verses += (j + 1) + ":";
                        verses += ((JObject)verserKeys[(j + 1).ToString()])["verse"] + "\n";
                    }

                    return  new BibleVerse()
                    {
                        Version = version,
                        ChapterNr = i + 1,
                        Chapter = chapterName,
                        Verses = verses
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
