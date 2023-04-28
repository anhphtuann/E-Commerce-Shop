namespace dotnet_rpg.Data
{
    public interface IAuthRepository {
        Task<ServiceResponse<int>> Register(User user, string password, string[] roles);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}