using EmployeeApiConsumer.Models;

namespace EmployeeApiConsumer.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://localhost:7181/";

        public AccountService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<AuthenticationResponse> Login(LoginEntity entity, string ipAddress)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "Login");
            request.Headers.Add("IpAddress", ipAddress);
            request.Content = JsonContent.Create(entity);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Register(User user)
        {
            user.CreatedBy = "Self";
            user.ModifiedBy = "Self";
            user.CreatedOn = DateTime.Now;
            user.ModifiedOn = DateTime.Now;
            user.GenderId = 10;
            user.Id = Guid.NewGuid().ToString();

            var response = await _client.PostAsJsonAsync("Register", user);

            return response.IsSuccessStatusCode;
        }
    }
}
