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
using Xamarin.Forms;

namespace BibleApp.Services
{
    public class RestService : IRestService<TodoItem>
    {
        HttpClient _client;
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://getbible.net" : "https://getbible.net";
        public static string TodoItemsUrl = BaseAddress + "/index.php?option=com_getbible&view=json&p={0}&v={1}";

        public List<TodoItem> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<IEnumerable<TodoItem>> RefreshDataAsynURI(string id)
        {
            Items = new List<TodoItem>();

            _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
            var uri = new Uri("https://api.github.com/search/repositories?q=pluralsight");
            //var uri = new Uri(string.Format(TodoItemsUrl, id.Split('_')[2], id.Split('_')[0]));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<IEnumerable<TodoItem>> RefreshDataAsync(string id)
        {
            Items = new List<TodoItem>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(TodoItemsUrl, id.Split('_')[2], id.Split('_')[0]))))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                using (var response = await _client.SendAsync(request).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                    using (var streamReader = new StreamReader(decompressedStream))
                    {
                        var result = await streamReader.ReadToEndAsync().ConfigureAwait(false);

                        //remove ( , ); from the string
                        string jsonResult = result.ToString().Remove(result.Length -3,2).Remove(0, 1);
                        //Items = JsonConvert.DeserializeObject<List<TodoItem>>(jsonResult);
                    }
                }
            }

            return Items;
        }
    }
}
