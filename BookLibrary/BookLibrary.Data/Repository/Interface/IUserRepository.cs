using BookLibrary.Model.Entities;

namespace BookLibrary.Data.Repository.Interface
{
    public interface IUserRepository
    {
        
        Task<User> FindUserById(Guid id);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUserById(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<IEnumerable<User>> GetAllUsers();
        //Task<IEnumerable<User>> GetAllUsers(int page, int pageSize);
        Task<IEnumerable<User>> GetAllUsers(int itemtoskip, int pagesize);

    }
}
