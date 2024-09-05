using EmployeeApiConsumer.Models;

namespace EmployeeApiConsumer.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> Login(LoginEntity entity, string ipAddress);
        Task<bool> Register(User user);
    }
}