using LexiconLMS.App.Client.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LexiconLMS.App.Client.Services
{
    public class LMSClient
    {
        private readonly HttpClient httpClient;

        public LMSClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<T>?> GetAsync<T>(string uri)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<T>>(uri);
        }

        public async Task<T?> GetByIdAsync<T>(string id, string uri)
        {
            return await httpClient.GetFromJsonAsync<T>($"{uri}/{id}");
        }

        public async Task<T?> PostAsync<T>(T createItem, string uri)
        {
            var response = await httpClient.PostAsJsonAsync(uri, createItem);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<T>() : default;
        }

        public async Task<bool> RemoveAsync(string id, string uri)
        {
            return (await httpClient.DeleteAsync($"{uri}/{id}")).IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync<T>(T item, string uri) where T: IEntityDto
        {
            return (await httpClient.PutAsJsonAsync($"{uri}/{item.Id}", item)).IsSuccessStatusCode;
        }
}
}
