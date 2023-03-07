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

        public async Task<IEnumerable<CourseDto>?> GetAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CourseDto>>("api/courses");
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
