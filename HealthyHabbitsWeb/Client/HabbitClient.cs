using EleniBlog.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace HealthyHabbitsWeb.Client
{
    
    public class HabbitClient
    {
        public event EventHandler DataChanged;

        HttpClient httpClient;
        public HabbitClient(HttpClient _http)
        {
            httpClient = _http;
        }
        public async Task<List<Recipe>> LoadAsync() =>
           await httpClient.GetFromJsonAsync<List<Recipe>>("habbits") ??
            new List<Recipe>();

        public async Task SaveHabbitAsync(Recipe recipe) { 
            var res =await httpClient.PutAsJsonAsync<Recipe>("habbits", recipe);

            if(res.IsSuccessStatusCode)
            {
                await LoadAsync();
                OnDataChanged();
            }
        }
     
        public async Task DeleteHabbitAsync(int id)
        {
            var uri = new Uri(httpClient.BaseAddress.ToString() +
                "habbits?Id=" + id);
            var res = await httpClient.DeleteAsync(uri);

            if (res.IsSuccessStatusCode)
            {
                await LoadAsync();
                OnDataChanged();
            }
           
        }

        public async Task InsertHabbitAsync(Recipe recipe)
        {
            if (recipe == null)
                return;

          //var jsonRecipe = System.Text.Json.JsonSerializer.Serialize(recipe);

           var res = await httpClient.PostAsJsonAsync("habbits", recipe);

            if (res.IsSuccessStatusCode)
            {
                await LoadAsync();
                OnDataChanged();
            }
            

        }
        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
