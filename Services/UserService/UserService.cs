using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using E_Commerce_Shop.Data;
using E_Commerce_Shop.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Shop.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ShopContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<UserProfile>> GetMyProfile() {
            ServiceResponse<UserProfile> s = new ServiceResponse<UserProfile>();
            try{
                var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == GetUserId());
                if(user is null){
                    throw new Exception("User not found");
                }
                s.Data = _mapper.Map<UserProfile>(user);
                s.Message = "Get my profile successfull";
            }catch(Exception ex) {
                s.Message = ex.Message;
            }
            return s;
        }
        public async Task<ServiceResponse<int>> CreateUser(CreateUserDto user)
        {
            User u = new User();
            var response = new ServiceResponse<int>();
            if(await UserExists(user.UserName)){
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            u.Username = user.UserName;
            u.Email = user.Email;
            u.PasswordHash = passwordHash;
            u.PasswordSalt = passwordSalt;
            u.Roles = user.Role;
            u.Contact = user.Contact;
            _context.User.Add(u);
            await _context.SaveChangesAsync();
            response.Data = _context.User.Max(u => u.UserId);
            return response;
        }
        public async Task<ServiceResponse<int>> ChangeContact(string Contact, string UserName){
            ServiceResponse<int> s = new ServiceResponse<int>();
            try{
                var data = await _context.User.FirstOrDefaultAsync(p => p.Username == UserName)!;
                if(data is null){
                    throw new Exception("Not valid User");
                }
                data!.Contact = Contact;
                s.Data = 1;
                s.Message = $"have 1 change";
                await _context.SaveChangesAsync();
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<int>> ChangeEmail(string email, string UserName){
            ServiceResponse<int> s = new ServiceResponse<int>();
            try{
                var data = await _context.User.FirstOrDefaultAsync(p => p.Username == UserName)!;
                if(data is null){
                    throw new Exception("Not valid User");
                }
                data!.Email = email;
                s.Data = 1;
                s.Message = $"have 1 email change";
                await _context.SaveChangesAsync();
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<int>> ChangePassword(string oldPassword, string newPassword, string UserName){
            ServiceResponse<int> s = new ServiceResponse<int>();
            try{
                if(! await (UserExists(UserName))){
                    throw new Exception("Account not valid");
                }
                    var data = await _context.User.FirstOrDefaultAsync(p => p.Username == UserName)!;
                    if(VerifyPasswordHash(oldPassword, data!.PasswordHash, data.PasswordSalt)){
                        CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                        data.PasswordHash = passwordHash;
                        data.PasswordSalt = passwordSalt;
                        s.Data = data.UserId;
                        s.Message = "Change password successfull";
                        await _context.SaveChangesAsync();

                    }else {
                        throw new Exception("Password not correct");
                    }
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<int>> ChangeUserName(string Password, string UserNameOld, string UserNameNew){
            ServiceResponse<int> s = new ServiceResponse<int>();
            try{
                if(! await (UserExists(UserNameOld))){
                    throw new Exception("Account not valid");
                }
                    var data = await _context.User.FirstOrDefaultAsync(p => p.Username == UserNameOld)!;
                    if(VerifyPasswordHash(Password, data!.PasswordHash, data.PasswordSalt)){
                        data.Username = UserNameNew;
                        s.Data = data.UserId;
                        s.Message = $"Change username {UserNameOld} to {UserNameNew} successfull";
                        await _context.SaveChangesAsync();

                    }else {
                        throw new Exception("Password not correct");
                    }
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<string>> ChangeAvartaByUserName(string UserName, string Avartar){
            ServiceResponse<string> s = new ServiceResponse<string>();
            try{
                if(! await (UserExists(UserName))){
                    throw new Exception("Account not valid");
                }
                var data = await _context.User.FirstOrDefaultAsync(p => p.Username == UserName)!;
                data!.Avartar = Avartar;
                await _context.SaveChangesAsync();
                s.Data = $"Change {Avartar} successful";
                s.Message = "Change avarta sucess";
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<ResponseUserWithId>> GetUserById(int id){
            ServiceResponse<ResponseUserWithId> s = new ServiceResponse<ResponseUserWithId>();
            try{
                var data = await _context.User.FirstOrDefaultAsync(p => p.UserId == id)!;
                if(data is null){
                    throw new Exception($"User with {id} not found");
                }
                s.Data = _mapper.Map<ResponseUserWithId>(data);
                s.Message = $"Get user with {id} successful";
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<bool> UserExists(string username)
        {
            if(await _context.User.AnyAsync(u => u.Username.ToLower() == username.ToLower())){
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public async Task<ServiceResponse<int>> UpdateUserById(BodyUpdateUserById body, int id){
            ServiceResponse<int> s = new ServiceResponse<int>();
            try{
                var data = await _context.User.FirstOrDefaultAsync(p => p.UserId == id)!;
                if(data is null) {
                    throw new Exception($"Account with {id} not found");
                }
                data!.Username = body.UserName;
                data!.Contact = body.Contact;
                s.Data = data.UserId;
                s.Message = $"Update user by {id} successful";
                await _context.SaveChangesAsync();
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<int>> DeleteUserById(int id){
            ServiceResponse<int> s = new ServiceResponse<int>();
            try{
                var data = await _context.User.FirstOrDefaultAsync(p => p.UserId == id)!;
                if(data is null) {
                    throw new Exception($"Delete account with {id} false");
                }
                this._context.User.Remove(data);
                await _context.SaveChangesAsync();
                s.Data = data.UserId;
                s.Message = $"Delete User With {id} successful";
            }catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
    }
}