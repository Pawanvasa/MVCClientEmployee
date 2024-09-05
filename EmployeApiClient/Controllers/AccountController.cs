using EmployeeApiConsumer.Models;
using EmployeeApiConsumer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;

namespace EmployeeApiConsumer.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;
        private readonly string baseUrl;
        private readonly IAccountService _accountServices;
        private readonly IConfiguration _configuration;


        public AccountController(IHttpClientFactory httpClientFactory, IAccountService accountServices, IConfiguration configuration)
        {
            this._accountServices = accountServices;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("BaseUrl");
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri(baseUrl);
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> UserLogin(LoginEntity entity)
        {
            try
            {
                var IPAddress = (HttpContext.Items["IpAddress"])!.ToString()!;
                var request = new HttpRequestMessage(HttpMethod.Post, "Login");
                request.Headers.Add("IpAddress", IPAddress);
                request.Content = new ObjectContent<LoginEntity>(entity, new JsonMediaTypeFormatter());
                var result = await _client.SendAsync(request);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseModel = await result.Content.ReadAsAsync<AuthenticationResponse>();
                    HttpContext.Session.SetString("token", responseModel.Token);
                    HttpContext.Session.SetString("UserName", entity.UserName);

                    return RedirectToAction("Navigation", "Comman");
                }
                else
                {
                    TempData["ErrorMsg"] = "Invalid Email or Password";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Registration()
        {
            return View();
        }
        public async Task<IActionResult> Register(User RegisterModel)
        {
            try
            {
                var result = await _accountServices.Register(RegisterModel);
                if (result)
                {
                    TempData["AlertMessage"] = "Registration Success";
                    return View(result);
                }
                return RedirectToAction("Login");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult UserLogout()
        {
            try
            {
                HttpContext.Session.Remove("token");
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
