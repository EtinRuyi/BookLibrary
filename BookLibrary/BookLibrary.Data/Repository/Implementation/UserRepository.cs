using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
       

        public async Task<bool> DeleteUserById(User user)
        {
            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<User> FindUserById(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers(int itemtoskip, int pagesize)
        {
            return await _userManager.Users.Skip(itemtoskip).Take(pagesize).ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User> UpdateUser(User user)
        {
           await _userManager.UpdateAsync(user);
            return user;
        }
    }
}
