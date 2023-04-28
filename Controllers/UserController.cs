using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Dtos.User;
using E_Commerce_Shop.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Shop.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpGet("my-profile")]
        public async Task<ActionResult<ServiceResponse<UserProfile>>> GetMyProfile() {
            var respones = await _userService.GetMyProfile();
            if(respones.Data is null){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<int>>> CreateUser([FromBody] CreateUserDto body) {
            var response = await _userService.CreateUser(body);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPatch("change-contact")]
        public async Task<ActionResult<ServiceResponse<int>>> changeContactByUserName([FromBody] string Contact, string UserName){
            var respones = await _userService.ChangeContact(Contact, UserName);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpPatch("change-email")]
        public async Task<ActionResult<ServiceResponse<int>>> changeEmailByUserName([FromBody] string email, string UserName){
            var respones = await _userService.ChangeEmail(email, UserName);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpPatch("change-password")]
        public async Task<ActionResult<ServiceResponse<int>>> changePasswordByUsername([FromBody] BodyChangePasswordDto data, string UserName){
            var respones = await _userService.ChangePassword(data.oldPassword, data.newPassword, UserName);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpPatch("change-user")]
        public async Task<ActionResult<ServiceResponse<int>>> changeUserNameByPaword([FromBody] BodyChangeUserName data, string password){
            var respones = await _userService.ChangeUserName(password, data.OldUserName, data.NewUserName);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpPatch("change-avatar")]
        public async Task<ActionResult<ServiceResponse<int>>> changeAvatarByUserName([FromBody] string avatar, string username){
            var respones = await _userService.ChangeAvartaByUserName(username, avatar);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> GetUserById(int id){
            var respones = await _userService.GetUserById(id);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> UpdateUserById([FromBody] BodyUpdateUserById body, int id){
            var respones = await _userService.UpdateUserById(body, id);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteUserById(int id){
            var respones = await _userService.DeleteUserById(id);
            if(!respones.Success){
                return BadRequest(respones);
            }
            return Ok(respones);
        }
    }
}