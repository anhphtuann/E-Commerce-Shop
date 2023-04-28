using API.Dtos.Users;
using API.Models.ServiceResponse;
using API.Models.Users;
using API.Service.Auths;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User {  firstName = request.firstName,
                            lastName = request.lastName,
                            account = request.account,
                            role = request.role 
                        }, 
                    request.password
                );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(request.account, request.password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}