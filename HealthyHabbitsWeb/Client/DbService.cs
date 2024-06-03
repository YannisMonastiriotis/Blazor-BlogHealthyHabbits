using HealthyHabbitsWeb.Data;
using Newtonsoft.Json;
using System.Net.Http;

namespace HealthyHabbitsWeb.Client
{
    public class DbService
    {
        public event EventHandler DataChanged;
        HttpClient httpClient;
        public DbService(HttpClient _http)
        {
            httpClient = _http;
        }
        public async Task<UserAccount> LoadUser(string? userName)
        {
          var userAcc =  new UserAccount();

            var uri = new Uri(httpClient.BaseAddress.ToString() +
              "getUser?UserName=" + userName);

            var responseMessage = await httpClient.GetAsync(uri);

            if (responseMessage == null) return null;
            if(!responseMessage.IsSuccessStatusCode) return null;
            var content = await responseMessage.Content.ReadAsStringAsync();

            userAcc = JsonConvert.DeserializeObject<UserAccount>(content);
               
            
            return userAcc;
        }
        
        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
