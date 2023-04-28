using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using E_Commerce_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Data{
    public class AuthRepository : IAuthRepository
    {
        private readonly ShopContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(ShopContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.User
                       .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if(user is null) {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)){
                response.Success = false;
                response.Message = "Wrong password";
            }else {
                response.Data = CreateToken(user);
            }  
            return response;      
        }

        public async Task<ServiceResponse<int>> Register(User user, string password, string[] roles)
        {
            var response = new ServiceResponse<int>();
            if(await UserExists(user.Username)){
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Roles = String.Join(",",roles);
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.UserId;
            return response;
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
        private string CreateToken(User user) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            var roles = user.Roles.Split(",");
            foreach(var role in roles){
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if(appSettingsToken is null){
                throw new Exception("AppSettings Token is null");
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingsToken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}