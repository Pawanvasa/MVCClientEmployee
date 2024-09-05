﻿using ConnectAndSell.Common;
using EmployeeApiConsumer.Helpers;
using EmployeeApiConsumer.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeApiConsumer.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        readonly HttpClient _client;
        readonly HttpHelper _httpHelper;
        readonly string baseUrl;
        readonly JwtHeaderHelper _jwtHelper;
        readonly IConfiguration _configuration;
        public EmployeeServices(JwtHeaderHelper jwtHeaderHelper, IConfiguration configuration, HttpHelper httpHelper)
        {
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("BaseUrl");
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri(baseUrl);
            _jwtHelper = jwtHeaderHelper;
            _jwtHelper.AddJwtToHeaders(_client.DefaultRequestHeaders);
            _httpHelper = httpHelper;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var result = await _client.GetAsync($"{baseUrl}GetAllEmployees");
            var responseModel = new List<Employee>();
            if (result.StatusCode== System.Net.HttpStatusCode.OK)
            {
                responseModel = await result.Content.ReadAsAsync<List<Employee>>();
            }
            else
            {
                throw new Exception("There are no Employee records");
            }
            return responseModel;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _client.GetFromJsonAsync<Employee>($"GetEmployeesById?id=" + employeeId);
            if (employee == null)
            {
                throw new Exception($"There are no Employee record found with id{employeeId}");
            }
            return employee;

        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            var result = await _client.PostAsJsonAsync($"CreateEmployee", employee);
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> EditEmployeeAsync(Employee employee)
        {
            var respose = await _client.PutAsJsonAsync<Employee>($"UpdateEmployee", employee);
            return respose.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> DeletEmployeeAsync(int id)
        {
            var respose = await _client.DeleteAsync($"DeleteEmployee?id=" + id);
            return respose.StatusCode == System.Net.HttpStatusCode.OK;
        }
        public async Task<bool> PatchEmployee(JsonPatchDocument patchDocument, int id)
        {
            var serializeDoc = JsonConvert.SerializeObject(patchDocument);
            var request = new StringContent(serializeDoc, Encoding.UTF8, "application/json-patch+json");
            var PatchUpdate = await _client.PatchAsync("PatchEmployee/" + id, request);
            return PatchUpdate.IsSuccessStatusCode;
        }
    }
}
