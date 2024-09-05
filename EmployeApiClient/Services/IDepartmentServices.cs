using EmployeeApiConsumer.Models;

namespace EmployeeApiConsumer.Services
{
    public interface IDepartmentServices
    {
        Task<List<Department>> GetDepartmentsAsync();
    }
}