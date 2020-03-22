using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class ApiReader : IApiReader
    {
        private readonly HttpClient _httpClient;

        public ApiReader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ReadAsString(string url)
        {
            InitRequestHeader();

            return await _httpClient.GetStringAsync(url);
        }

        public async Task<T> ReadFromUrlAsync<T>(string url) where T : class
        {
            InitRequestHeader();

            var streamResult = await _httpClient.GetStreamAsync(url);

            return await JsonSerializer.DeserializeAsync<T>(streamResult);
        }

        private void InitRequestHeader()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
