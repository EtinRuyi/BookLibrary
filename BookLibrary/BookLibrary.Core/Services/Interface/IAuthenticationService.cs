using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginRequestDto loginRequestDto);
        Task<BaseResponse<UserRegDTO>> Register(UserRegDTO userreg);

    }
}
