using BookLibrary.Core.Services.Interface;
using BookLibrary.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers.UserController
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> GetAllUsers(int pagenumber=1, int pagesize=10)
        {
           var allusers = await _userService.GetAllUsers(pagenumber,pagesize);
            return Ok(allusers);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        //[HttpPost]
        //[Route("CreateAccount")]
        //public async Task<IActionResult> CreateUserAccount(UserRegDTO userRegDTO)
        //{
        //    return Ok(await _userService.AddUser(userRegDTO));
        //}

        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<IActionResult> UpdateUserAccount(UserRegDTO userRegDTO, Guid id)
        {
            return Ok(await _userService.UpdateUser(userRegDTO, id));
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            return Ok(await _userService.DeleteUser(id));
        }
    }
}
