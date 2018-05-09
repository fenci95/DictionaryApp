using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DictionaryApp.Models;
using Xamarin.Forms;
using System.Net;

namespace DictionaryApp.Services
{
    public class DictionaryService
    {
        HttpClient client;
        string id = "1923e3f1";
        string key = "4420d2b379b99a726ab32554d6e90800";
        public DictionaryService()
        {

        }

        private readonly Uri serverUrl = new Uri("https://od-api.oxforddictionaries.com:443");
        private async Task<T> GetAsync<T>(Uri uri)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("app_id", id);
            client.DefaultRequestHeaders.Add("app_key", key);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            else{
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    DependencyService.Get<IMessage>().LongAlert("No entry is found!");
                    return default(T);
                }
                else
                {
                        DependencyService.Get<IMessage>().LongAlert("An error occurred while processing the data!");
                        return default(T);
                }
            }
        }        public async Task<Languages> GetLanguagesAsync()
        {
            return await GetAsync<Languages>(new Uri(serverUrl, "/api/v1/languages"));
        }        public async Task<Synonyms> GetSynonymsAsync(string uri)
        {
            return await GetAsync<Synonyms>(new Uri(serverUrl, uri));
        }        public async Task<Translations> GetTranslationsAsync(string uri)
        {
            return await GetAsync<Translations>(new Uri(serverUrl, uri));
        }
    }
}
