using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Dtos.User;

namespace E_Commerce_Shop.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<UserProfile>> GetMyProfile();
        Task<ServiceResponse<int>> CreateUser(CreateUserDto user);
        Task<ServiceResponse<int>> ChangeContact(string Contact, string UserName);
        Task<ServiceResponse<int>> ChangeEmail(string email, string UserName);
        Task<ServiceResponse<int>> ChangePassword(string oldPassword, string newPassword, string UserName);
        Task<ServiceResponse<int>> ChangeUserName(string Password, string UserNameOld, string UserNameNew);
        Task<ServiceResponse<string>> ChangeAvartaByUserName(string UserName, string Avartar);
        Task<ServiceResponse<ResponseUserWithId>> GetUserById(int id);
        Task<ServiceResponse<int>> UpdateUserById(BodyUpdateUserById body, int id);
        Task<ServiceResponse<int>> DeleteUserById(int id);
    }
}