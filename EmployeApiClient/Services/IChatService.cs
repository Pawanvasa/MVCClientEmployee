using EmployeeApiConsumer.Models;

namespace EmployeeConsumer.Services
{
    public interface IChatService
    {
        Task<List<User>> GetUsersAsync();
    }
}