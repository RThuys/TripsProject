using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace uwp_app.Services
{
    class DatabaseHelper
    {
        public static string URL = "http://127.0.0.1:53025/api";

        public static async Task<string> CreateClient(string url)
        {
            url = URL + url;
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            return response;
        }

        public static async void Post(string path, string json)
        {
            HttpClient client = new HttpClient();
            await client.PostAsync(URL + path,
            new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public static async void Put(string path, string json)
        {
            HttpClient client = new HttpClient();
            await client.PutAsync(URL + path,
            new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public static async void Delete(string path, string json)
        {
            HttpClient client = new HttpClient();
            await client.DeleteAsync(URL + path + $"/{json}");
        }
    }
}
