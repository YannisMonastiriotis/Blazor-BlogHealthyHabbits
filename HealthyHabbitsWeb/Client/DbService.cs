using HealthyHabbitsWeb.Data;
using HealthyHabbitsWeb.Data.ViewModels;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using static HealthyHabbitsWeb.Components.Pages.Account.Signup;
using static System.Net.WebRequestMethods;


namespace HealthyHabbitsWeb.Client
{
    public class DbService
    {
        public event EventHandler DataChanged;
        HttpClient httpClient;
        IConfiguration _configuration;
        public DbService(HttpClient _http, IConfiguration configuration)
        {
            httpClient = _http;
            _configuration = configuration;
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
        public async Task<UserAccount> GetUserByEmail(string? email)
        {
            var userAcc = new UserAccount();

            var uri = new Uri(httpClient.BaseAddress.ToString() +
              "getUserByEmail?Email=" + email);

            var responseMessage = await httpClient.GetAsync(uri);

            if (responseMessage == null) return null;
            if (!responseMessage.IsSuccessStatusCode) return null;
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

        public async Task<bool>  UpdateUserEmailToConfirmed(UserAccount userAccount)
        {
            // Send data to the backend
            userAccount.IsEmailConfirmed = true;
            var response = await httpClient.PostAsJsonAsync("EmailConfirmed", userAccount);

            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            else
            {
                return false;


            }
        }

        public async Task<bool> InsertUser(SignUpViewModel Model)
        {
            Model.IsEmailConfirmed = false;
            Model.Role = "user";
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
        private static string Subject { get; set; } = "Welcome " + User + "please confirm your email";
     
        private static string User { get; set; }
        public async Task<bool> SendEmail(string to, string baseUri)
        {
            User = to;
            
            var confirmationLink = baseUri + $"confirm-email?email={to}"; // Adjust the token parameter as needed
            
            var body = $"Please click the following link to confirm your email address: <a href=\"{confirmationLink}\">{confirmationLink}</a>";
            var emailUsername = _configuration["EmailCredentials:Username"];
            var emailPassword = _configuration["EmailCredentials:Password"];

            //var emailUsername = Environment.GetEnvironmentVariable("EmailCredentials:Username");
            //var emailPassword = Environment.GetEnvironmentVariable("EmailCredentials:Password");

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(emailUsername, emailPassword),
                EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = false
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailUsername),
                Subject = Subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            try
            {
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (SmtpException ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
