using HealthyHabbitsWeb.Data;
using HealthyHabbitsWeb.Data.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using static HealthyHabbitsWeb.Components.Pages.Account.Signup;
using static System.Net.WebRequestMethods;

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
        
        public async Task<bool> EmailExists (string email)
        {

            var uri = new Uri(httpClient.BaseAddress.ToString() +
             "emailExists?Email=" + email);
            // Replace with your actual API key
            var response = await httpClient.GetFromJsonAsync<EmailVerificationResponse>(uri);
            var resp = response?.IsValid ?? false;
           return resp;
        }

        public async Task<bool> InsertUser(SignUpViewModel Model)
        {
            // Send data to the backend
            var response = await httpClient.PostAsJsonAsync("signUp", Model);

            if (response.IsSuccessStatusCode)
            {
                return true;
                // Handle successful response
               
            }
            else
            {
                return false;
                
                
            }
        }
        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
