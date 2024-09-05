using EmployeeApiConsumer.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace EmployeeApiConsumer.Services
{
    public interface IEmployeeServices
    {
        Task<bool> AddEmployeeAsync(Employee employee);
        Task<bool> DeletEmployeeAsync(int id);
        Task<bool> EditEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<List<Employee>> GetEmployeesAsync();
        Task<bool> PatchEmployee(JsonPatchDocument patchDocument, int id);
    }
}