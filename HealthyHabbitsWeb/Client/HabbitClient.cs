using EleniBlog.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
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

        public async Task<List<Recipe>> LoadAsync(string recipe)
        {
           var habbits = await httpClient.GetFromJsonAsync<List<Recipe>>("habbits");
              List<Recipe>  recipesToReturn = new List<Recipe>();

            foreach(var habbit in habbits)
            {
                
                if(!habbit.Title.IsNullOrEmpty())
                {
                    if (habbit.Title.Contains(recipe,StringComparison.OrdinalIgnoreCase))
                    {
                        if(!recipesToReturn.Any(rspTitle => rspTitle.Id == habbit.Id))
                        {
                            habbits.Add(habbit);
                        }
                    };
                }


                if (!habbit.Description.IsNullOrEmpty())
                {
                    if (habbit.Description.Contains(recipe, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!recipesToReturn.Any(rspTitle => rspTitle.Id == habbit.Id))
                        {
                            habbits.Add(habbit);
                        }
                    };
                }

            }

            return recipesToReturn.Any() ? recipesToReturn.OrderByDescending(x => x.CreatedDateTime).ToList() : recipesToReturn;

        }

        public async Task<List<Recipe>> LoadAsync(DateTime date)
        {
            var habbits = await httpClient.GetFromJsonAsync<List<Recipe>>("habbits");
            List<Recipe> recipesToReturn = new List<Recipe>();

            foreach (var habbit in habbits)
            {

                if (habbit.CreatedDateTime != null)
                {
                    if (habbit.CreatedDateTime.ToString().Contains(date.ToString()))
                    {
                        if (!recipesToReturn.Any(rspTitle => rspTitle.Id == habbit.Id))
                        {
                            habbits.Add(habbit);
                        }
                    };
                }
            }

            return recipesToReturn.Any() ? recipesToReturn.OrderByDescending(x => x.CreatedDateTime).ToList() : recipesToReturn;

        }

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
