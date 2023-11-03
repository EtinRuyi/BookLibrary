using BookLibrary.Core.Services.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authenticationService.Login(loginRequestDto));
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegDTO userRegDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authenticationService.Register(userRegDTO));
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authenticationService.Logout();

            if (result.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return StatusCode(result.ResponseCode, new { Message = result.Message });
            }
        }

    }
}
