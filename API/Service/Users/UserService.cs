using System.Security.Claims;
using API.Dtos.Users;
using API.Models.ServiceResponse;
using AutoMapper;

namespace API.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ShopContext _shopContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IMapper mapper, ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _shopContext = shopContext;
            _mapper = mapper;
            
        }
        public async Task<ServiceResponse<string>> DeleteProfileById(int id)
        {
            var response = new ServiceResponse<string>();
            try
            {                         
                var user = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == id);
                
                if (user is null)
                {
                    throw new Exception($"User has id {id} not found");
                }
                
                _shopContext.Users.Remove(user);

                await _shopContext.SaveChangesAsync();
                
                response.Data = $"User has id {id} deleted";
                  
                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetAllUserDto>>> GetAllUserProfile()
        {
            var response = new ServiceResponse<List<GetAllUserDto>>();
            try
            {                         
                var resultQuery = await _shopContext.Users
                                    .ToListAsync();
                
                var result = resultQuery
                                .Select(u => _mapper.Map<GetAllUserDto>(u))
                                .ToList();
                
                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetMyProfile()
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {                         
                var resultQuery = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                var result = _mapper.Map<GetUserDto>(resultQuery);

                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetAllUserDto>> GetUserProfileById(int id)
        {
            var response = new ServiceResponse<GetAllUserDto>();
            try
            {                         
                var user = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == id);
                
                if (user is null)
                {
                    throw new Exception($"User has id {id} not found");
                }
                
                var result = _mapper.Map<GetAllUserDto>(user);
                
                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateAccount(string newAccount)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {                         
                var user = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                if (newAccount is not null)
                {
                    user.account = newAccount;
                }
                else
                {
                    throw new Exception("New account is null");
                }

                await _shopContext.SaveChangesAsync();
                
                var resultQuery = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                var result = _mapper.Map<GetUserDto>(resultQuery);
                
                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Task<ServiceResponse<GetUserDto>> UpdateAvatar(string newUrlAvatar)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateContact(string newContact)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {                         
                var user = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                if (newContact is not null)
                {
                    user.contact = newContact;
                }
                else
                {
                    throw new Exception("New contact is null");
                }

                await _shopContext.SaveChangesAsync();
                
                var resultQuery = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                var result = _mapper.Map<GetUserDto>(resultQuery);
                
                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateEmail(string newEmail)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {                         
                var user = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                if (newEmail is not null)
                {
                    user.email = newEmail;
                }
                else
                {
                    throw new Exception("New email is null");
                }

                await _shopContext.SaveChangesAsync();
                
                var resultQuery = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                var result = _mapper.Map<GetUserDto>(resultQuery);
                
                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdatePassword(string oldPassword, string newPassword)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {                         
                var user = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                if (newPassword is not null && oldPassword is not null)
                {
                    if (VerifyPasswordHash(oldPassword, user.passwordHash, user.passwordSalt))
                    {
                        CreatePasswordHash(newPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);

                        user.passwordHash = newPasswordHash;
                        user.passwordSalt = newPasswordSalt;
                    }
                    else
                    {
                        throw new Exception("Wrong old password");
                    }
                }
                else
                {
                    throw new Exception("Old password or new password is null");
                }

                await _shopContext.SaveChangesAsync();
                
                var resultQuery = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == GetUserId());
                
                var result = _mapper.Map<GetUserDto>(resultQuery);
                
                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetAllUserDto>> UpdateUserProfileById(int id, UpdateUserDto updateUser)
        {
            var response = new ServiceResponse<GetAllUserDto>();
            try
            {                         
                var user = await _shopContext.Users
                                    .FirstOrDefaultAsync(u => u.userId == id);
                
                if (user is null)
                {
                    throw new Exception($"User has id {id} not found");
                }
                
                user.firstName = (updateUser.firstName is not null) ? updateUser.firstName : user.firstName;
                user.lastName = (updateUser.lastName is not null) ? updateUser.lastName : user.lastName;
                user.account = (updateUser.account is not null) ? updateUser.account : user.account;
                user.email = (updateUser.email is not null) ? updateUser.email : user.email;
                user.contact = (updateUser.contact is not null) ? updateUser.contact : user.contact;

                if (updateUser.oldPassword is not null && updateUser.newPassword is not null)
                {
                    if (VerifyPasswordHash(updateUser.oldPassword, user.passwordHash, user.passwordSalt))
                    {
                        CreatePasswordHash(updateUser.newPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);

                        user.passwordHash = newPasswordHash;
                        user.passwordSalt = newPasswordSalt;
                    }
                    else
                    {
                        throw new Exception("Wrong old password");
                    }
                }
                

                await _shopContext.SaveChangesAsync();

                var resultQuery = await _shopContext.Users
                                        .FirstOrDefaultAsync(u => u.userId == id);
                
                var result = _mapper.Map<GetAllUserDto>(resultQuery);
                
                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext!.User
                .FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}