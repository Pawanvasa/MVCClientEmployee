using EmployeeApiConsumer.Helpers;
using EmployeeApiConsumer.Models;

namespace EmployeeConsumer.Services
{
    public class ChatService : IChatService
    {
        readonly HttpClient _client;
        readonly string baseUrl;
        readonly JwtHeaderHelper _jwtHelper;
        private readonly IConfiguration _configuration;
        public ChatService(JwtHeaderHelper jwtHeaderHelper, IConfiguration configuration)
        {
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("BaseUrl");
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri(baseUrl);
            _jwtHelper = jwtHeaderHelper;
            _jwtHelper.AddJwtToHeaders(_client.DefaultRequestHeaders);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var Users = await _client.GetFromJsonAsync<List<User>>($"GetallUsers");
            return Users ?? throw new Exception("There are no Users");
        }

    }
}
