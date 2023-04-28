using API.Models.ServiceResponse;
using API.Models.Users;

namespace API.Service.Auths
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string username);
    }
}