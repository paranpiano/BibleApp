using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public static string TodoItemsUrl = BaseAddress + "/index.php?option=com_getbible&view=json&p={0}&v={1}";
        private bool Chapter;

        public List<BibleVerse> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<IEnumerable<BibleVerse>> RefreshDataAsynURI(string id)
        {
            Items = new List<BibleVerse>();

            _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
            var uri = new Uri("https://api.github.com/search/repositories?q=pluralsight");
            //var uri = new Uri(string.Format(TodoItemsUrl, id.Split('_')[2], id.Split('_')[0]));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<BibleVerse>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<IEnumerable<BibleVerse>> RefreshDataAsync(string id)
        {
            Items = new List<BibleVerse>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(TodoItemsUrl, id.Split('_')[2], id.Split('_')[0]))))
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

                            for (int i = 0; i < chaterKeys.Count; i++)
                            {
                                var chapter = (JObject)book[(i + 1).ToString()];
                                var verserKeys =((JObject)book[(i + 1).ToString()])["chapter"].ToObject<Dictionary<string, object>>();

                                for (int j = 0; j < verserKeys.Count; j++)
                                {
                                    var verse = ((JObject)verserKeys[(j + 1).ToString()])["verse"];
                                    Items.Add(new BibleVerse()
                                    {
                                        Number = (j+1),
                                        Content = verse.ToString()
                                    });
                                }

                                break; //get only the first chapter
                            }

                            //Items = JsonConvert.DeserializeObject<List<BibleVerse>>(json.book.ToString());

                        }
                    }

                }
                catch (Exception)
                {
                    Items.Add(new BibleVerse()
                    {
                        Number = 1,
                        Content = "Failed to Get bible verses, please check network!"
                    });
                }
            }

            return Items;
        }
    }
}
