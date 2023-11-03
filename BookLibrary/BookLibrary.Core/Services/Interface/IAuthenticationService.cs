using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookLibrary.Core.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<string>> Login(LoginRequestDto loginRequestDto);
        Task<BaseResponse<UserRegDTO>> Register(UserRegDTO userreg);
        Task<BaseResponse<string>> Logout();
    }
}
