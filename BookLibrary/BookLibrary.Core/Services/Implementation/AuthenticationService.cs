using BookLibrary.Core.Services.Interface;
using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using BookLibrary.Model.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;

namespace BookLibrary.Core.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepo;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        IConfiguration _config;
        public AuthenticationService(IConfiguration config, UserManager<User> userManager, IAuthenticationRepository authenticationRepo, SignInManager<User> signInManager)
        {
            _authenticationRepo = authenticationRepo;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        public async Task<BaseResponse<UserRegDTO>> Register(UserRegDTO userreg)
        {

            var response = new BaseResponse<UserRegDTO>();
            try
            {
                var userfound = await _authenticationRepo.GetUserByEmail(userreg.Email);
                if (userfound != null)
                {
                    response.Message = "User email already exists";
                    response.IsSuccessful = false;
                    response.ResponseCode = 400;
                }
                else
                {
                    var user = new User
                    {
                        FirstName = userreg.FirstName,
                        LastName = userreg.LastName,
                        Email = userreg.Email,
                        Gender = userreg.Gender,
                        PhoneNumber = userreg.PhoneNumber,
                        UserName = userreg.UserName
                    };
                    if (await _authenticationRepo.AddUser(user, userreg.Password))
                    {
                        response.Message = "User registered successfully";
                        response.Data = userreg;
                        response.IsSuccessful = true;
                        response.ResponseCode = 200;
                    }
                }
                return response;
            }
            catch (Exception e)
            {

                response.Message = "Error: " + e;
                response.IsSuccessful = true;
                response.ResponseCode = 200;
                return response;
            }
        }

        //public async Task<string> Login(LoginRequestDto loginRequestDto)
        //{
        //    var user = await _authenticationRepo.GetUserByEmail(loginRequestDto.Email);
        //    if(user != null)
        //    {
        //        var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password,lockoutOnFailure:false);
        //        if (result.Succeeded)
        //        {
        //            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        //            return GenerateJwtToken(user, role);
        //        }
        //    }
        //    return "Invalid login details";
        //}

        public async Task<BaseResponse<string>> Login(LoginRequestDto loginRequestDto)
        {
            var response = new BaseResponse<string>();

            try
            {
                var user = await _authenticationRepo.GetUserByEmail(loginRequestDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                        response.Data = GenerateJwtToken(user, role);
                        response.IsSuccessful = true;
                        response.ResponseCode = 200;
                        response.Message = "Login successful";
                    }
                    else
                    {
                        response.IsSuccessful = false;
                        response.ResponseCode = 401;
                        response.Message = "Invalid login details";
                    }
                }
                else
                {
                    response.IsSuccessful = false;
                    response.ResponseCode = 401;
                    response.Message = "Invalid login details";
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.ResponseCode = 500;
                response.Message = "Error: " + e.Message;
            }

            return response;
        }


        public async Task<BaseResponse<string>> Logout()
        {
            var response = new BaseResponse<string>();

            try
            {
                await _authenticationRepo.Logout();
                response.IsSuccessful = true;
                response.ResponseCode = 200;
                response.Message = "Logged out successfully";
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.ResponseCode = 500;
                response.Message = "Error: " + e.Message;
            }

            return response;
        }


        private string GenerateJwtToken(User contact, string roles)
        {
            var jwtSettings = _config.GetSection("Jwt:Key").Value;
            //  getsection("jwt:key").value
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, contact.UserName),
        new Claim(JwtRegisteredClaimNames.Email, contact.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, roles)
    };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
