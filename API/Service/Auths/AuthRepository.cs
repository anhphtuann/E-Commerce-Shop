using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Models.ServiceResponse;
using API.Models.Users;
using Microsoft.IdentityModel.Tokens;

namespace API.Service.Auths
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ShopContext _shopContext;
        private readonly IConfiguration _configuration;
        public AuthRepository(ShopContext shopContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _shopContext = shopContext;
            
        }
        public async Task<ServiceResponse<string>> Login(string account, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _shopContext.Users
                .FirstOrDefaultAsync(u => u.account.ToLower().Equals(account.ToLower()));
            if (user == null) 
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else 
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExists(user.account))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            if (user.role != "consumer" && user.role != "vendor")
            {
                throw new Exception("Wrong account role");
            }    

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            
            _shopContext.Users.Add(user);
            await _shopContext.SaveChangesAsync();     
            response.Data = user.userId;

            return response;
        }

        public async Task<bool> UserExists(string account)
        {
            if (await _shopContext.Users.AnyAsync(u => u.account.ToLower() == account.ToLower())) 
            {
                return true;
            }
            return false;
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

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Name, user.account),
                new Claim(ClaimTypes.Role, user.role)
            };

            var appSettingToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingToken is null) 
            {
                throw new Exception("AppSettings Token is null!");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}