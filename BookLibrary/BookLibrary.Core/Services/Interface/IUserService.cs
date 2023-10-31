using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities.Shared;

namespace BookLibrary.Core.Services.Interface
{
    public interface IUserService
    {
        //Task<BaseResponse<UserRegReturnDTO>> AddUser(UserRegDTO userRegDTO);
        Task<BaseResponse<UserRegReturnDTO>> UpdateUser(UserRegDTO userRegDto, Guid id);
        Task<BaseResponse<bool>> DeleteUser(Guid id);
        Task<BaseResponse<IEnumerable<UserReturnDto>>> GetAllUsers(int pagenumber, int pageSize);
        Task<BaseResponse<UserReturnDto>> GetUserById(Guid id);


    }
}
