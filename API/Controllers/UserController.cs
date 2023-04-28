using API.Dtos.Users;
using API.Models.ServiceResponse;
using API.Service.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("my-profile")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetMyProfile()
        {
            var response = await _userService.GetMyProfile();
            return Ok(response);
        }

        [HttpPatch("change-password")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdatePassWord(string oldPassword, string newPassword)
        {
            var response = await _userService.UpdatePassword(oldPassword, newPassword);
            return Ok(response);
        }

        [HttpPatch("change-contact")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateContact(string contact)
        {
            var response = await _userService.UpdateContact(contact);
            return Ok(response);
        }

        [HttpPatch("change-email")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateEmail(string email)
        {
            var response = await _userService.UpdateEmail(email);
            return Ok(response);
        }

        [HttpPatch("change-account")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateAccount(string account)
        {
            var response = await _userService.UpdateAccount(account);
            return Ok(response);
        }

        [HttpPatch("change-avatar")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateAvatar(string urlAvatar)
        {
            var response = await _userService.UpdateAvatar(urlAvatar);
            return Ok(response);
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<GetAllUserDto>>>> GetAllUserProfile()
        {
            var response = await _userService.GetAllUserProfile();
            return Ok(response);
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserProfileById(int id)
        {
            var response = await _userService.GetUserProfileById(id);
            return Ok(response);
        }

        [Authorize(Roles = "administrator")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAllUserDto>>> UpdateUserProfileById(int id, UpdateUserDto updateUser)
        {
            var response = await _userService.UpdateUserProfileById(id, updateUser);
            return Ok(response);
        }

        [Authorize(Roles = "administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> DeleteProfileById(int id)
        {
            var response = await _userService.DeleteProfileById(id);
            return Ok(response);
        }



    }
}