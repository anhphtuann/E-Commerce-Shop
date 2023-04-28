using API.Dtos.Users;
using API.Models.ServiceResponse;

namespace API.Service.Users
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> GetMyProfile();
        Task<ServiceResponse<List<GetAllUserDto>>> GetAllUserProfile();
        Task<ServiceResponse<GetUserDto>> UpdatePassword(string oldPassword, string newPassword);
        Task<ServiceResponse<GetUserDto>> UpdateContact(string newContact);
        Task<ServiceResponse<GetUserDto>> UpdateEmail(string newEmail);
        Task<ServiceResponse<GetUserDto>> UpdateAccount(string newAccount);
        Task<ServiceResponse<GetUserDto>> UpdateAvatar(string newUrlAvatar);
        Task<ServiceResponse<GetAllUserDto>> GetUserProfileById(int id);
        Task<ServiceResponse<GetAllUserDto>> UpdateUserProfileById(int id, UpdateUserDto updateUser);
        Task<ServiceResponse<string>> DeleteProfileById(int id);
    }
}