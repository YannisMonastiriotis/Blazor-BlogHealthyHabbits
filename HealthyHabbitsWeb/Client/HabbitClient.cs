using EleniBlog.Data;
using System.Net.Http;

namespace HealthyHabbitsWeb.Client
{
    
    public class HabbitClient
    {
        HttpClient httpClient;
        public HabbitClient(HttpClient _http)
        {
            httpClient = _http;
        }
        public async Task<List<Recipe>> LoadAsync() =>
           await httpClient.GetFromJsonAsync<List<Recipe>>("habbits") ??
            new List<Recipe>();

        public async Task SaveHabbitAsync(Recipe recipe) =>
            await httpClient.PutAsJsonAsync<Recipe>("habbits", recipe);
     
        public async Task DeleteHabbitAsync(int id)
            => await httpClient.DeleteAsync($"habbits/{id}");
      
        public async Task InsertHabbitAsync(Recipe recipe)
            => await httpClient.PostAsJsonAsync("habbits", recipe);
        
    }
}
