using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookLibrary.Data.Repository.Implementation
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthenticationRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> AddUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);//Create user asynchronously
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

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }
    }
}
