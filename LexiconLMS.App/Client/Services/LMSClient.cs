using LexiconLMS.App.Client.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LexiconLMS.App.Client.Services
{
    public class LMSClient<T> where T : class
    {
        private readonly HttpClient httpClient;

        public LMSClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<T>?> GetAsync(string uri)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<T>>(uri);
        }

        //public async Task<ApplicationUser?> PostAsync(ApplicationUser createItem)
        //{
        //    var response = await httpClient.PostAsJsonAsync("api/todos", createItem);
        //    return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ApplicationUser>() : null;
        //}

        //public async Task<bool> RemoveAsync(string id)
        //{
        //    return (await httpClient.DeleteAsync($"api/todos/{id}")).IsSuccessStatusCode;
        //}

        //public async Task<bool> PutAsync(ApplicationUser item)
        //{
        //    return (await httpClient.PutAsJsonAsync($"api/todos/{item.Id}", item)).IsSuccessStatusCode;
        //}
    }
}
