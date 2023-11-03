using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using BookLibrary.Model.Entities.Shared;
using Microsoft.AspNetCore.Identity;

namespace BookLibrary.Data.Repository.Implementation
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public AuthenticationRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<bool> AddUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "RegularUser");
                return true;
            }
            return false;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<User> Login(LoginRequestDto login)
        {
            return await _userManager.FindByEmailAsync(login.Email);
        }

        public async Task<bool> Logout()
        {
            if (_signInManager != null)
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
