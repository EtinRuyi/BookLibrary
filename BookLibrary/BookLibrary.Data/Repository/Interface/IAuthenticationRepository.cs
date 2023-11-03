using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Data.Repository.Interface
{
    public interface IAuthenticationRepository
    {
        Task<bool> AddUser(User user, string password);
        Task<bool> Logout();
        Task<User> GetUserByEmail(string email);
    }
}
