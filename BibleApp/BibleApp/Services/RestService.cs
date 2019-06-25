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
        public DataContext _dc;
        HttpClient _client;
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://getbible.net" : "https://getbible.net";
        public static string TodoItemsUrl = BaseAddress + "/index.php?option=com_getbible&view=json&p={0}&v={1}";


        public RestService()
        {
            _client = new HttpClient();
        }

        public void SetDataContext(DataContext dc)
        {
            _dc = dc;
        }


        public async Task<IEnumerable<BibleVerse>> CreateBibleVerseData(string id)
        {
            string chapterName = id.Split('_')[2];
            string version = id.Split('_')[0];

            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(TodoItemsUrl, chapterName, version))))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                try
                {
                    using (var response = await _client.SendAsync(request).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
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
                                var verserKeys =((JObject)book[(i + 1).ToString()])["chapter"].
                                    ToObject<Dictionary<string, object>>();

                                // Verse # loop
                                var verses = "";
                                for (int j = 0; j < verserKeys.Count; j++)
                                {
                                    verses += (j + 1) + ":"; 
                                    verses += ((JObject)verserKeys[(j + 1).ToString()])["verse"] + "\n";
                                }

                                _dc.AddBibleVerse(new BibleVerse()
                                {
                                    Version = version,
                                    ChapterNr = i+1,
                                    Chapter = chapterName,
                                    Verses = verses
                                });
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return null;
        }
    }
}
