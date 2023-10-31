using BookLibrary.Core.Services.Interface;
using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using BookLibrary.Model.Entities.Shared;

namespace BookLibrary.Core.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
  /*      public async Task<BaseResponse<UserRegReturnDTO>> AddUser(UserRegDTO userRegDTO)
        {
            var response = new BaseResponse<UserRegReturnDTO>();
            try
            {           

                var checkUser = await _userRepository.GetUserByEmail(userRegDTO.Email);
                if (checkUser == null)
                {
                    var user = new User
                    {
                        Email = userRegDTO.Email,
                        FirstName = userRegDTO.FirstName,
                        LastName = userRegDTO.LastName,
                        Gender = userRegDTO.Gender,
                        UserName = userRegDTO.UserName,
                        PhoneNumber = userRegDTO.PhoneNumber,
                    };
                    await _userRepository.AddUser(user, userRegDTO.Password);
                    var returnResponse = new UserRegReturnDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        PhoneNumber = user.PhoneNumber,
                    };

                    response.Message = "User Added successfully";
                    response.ResponseCode = 201;
                    response.IsSuccessful = true;
                    response.Data = returnResponse;
                }
                else
                {
                    response.Message = "Email already exist";
                    response.ResponseCode = 400;
                    response.IsSuccessful = false;
                }
               
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.ResponseCode = 500;
                response.IsSuccessful = false;
                
            }
            return response;
        }*/

        public async Task<BaseResponse<bool>> DeleteUser(Guid id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var checkUser = await _userRepository.FindUserById(id);
                if (checkUser != null)
                {
                    await _userRepository.DeleteUserById(checkUser);
                    response.IsSuccessful = true;
                    response.ResponseCode =200;
                    response.Message = $"User deleted successfully";
                }
                response.IsSuccessful = false;
                response.ResponseCode = 404;
                response.Message = $"User not Found";
            }
            catch (Exception ex)
            {

                response.ResponseCode=500;
                response.IsSuccessful = false;
                response.Message= ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<UserReturnDto>>> GetAllUsers(int pagenumber, int pageSize)
        {
            int itemstoskip = (pagenumber-1) * pageSize;
            var users = await _userRepository.GetAllUsers(itemstoskip,pageSize);                

            List<UserReturnDto> userRegDTOs = new List<UserReturnDto>();
            var response = new BaseResponse<IEnumerable<UserReturnDto>>();
            foreach (var user in users)
            {
                
                userRegDTOs.Add(new UserReturnDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    PhoneNumber = user.PhoneNumber,
                });
            }
            response.Message = "Users Successfully Returned";
            response.IsSuccessful = true;   
            response.ResponseCode = 200;
            response.Data = userRegDTOs;
           return response;
        }

        public async Task<BaseResponse<UserReturnDto>> GetUserById(Guid id)
        {
            var response = new BaseResponse<UserReturnDto>();
            var checkdb = await _userRepository.FindUserById(id);
            if(checkdb != null)
            {
                var user = new UserReturnDto
                {
                    Id = checkdb.Id,
                    FirstName = checkdb.FirstName,
                    LastName = checkdb.LastName,
                    Gender = checkdb.Gender,
                    Email = checkdb.Email,
                    PhoneNumber = checkdb.PhoneNumber,
                    UserName = checkdb.UserName,
                };

                response.IsSuccessful = true;
                response.ResponseCode = 200;
                response.Message = "User found successfully";
                response.Data = user;
            }
            else
            {
                response.IsSuccessful = false;
                response.ResponseCode = 404;
                response.Message = $"User with {id} not found";
            }
            return response;
        }

        public async Task<BaseResponse<UserRegReturnDTO>> UpdateUser(UserRegDTO userRegDto, Guid id)
        {
            var response = new BaseResponse<UserRegReturnDTO>();
            var getUser = await _userRepository.FindUserById(id);
            if(getUser != null)
            {
                getUser.Email = userRegDto.Email;
                getUser.FirstName = userRegDto.FirstName;
                getUser.LastName = userRegDto.LastName;
                getUser.PhoneNumber = userRegDto.PhoneNumber;
                getUser.UserName = userRegDto.UserName;
                var user = await _userRepository.UpdateUser(getUser);

                var userReturnDto = new UserRegReturnDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Password = userRegDto.Password
                };
                
                response.IsSuccessful = true;
                response.Message = "Details Updated Successfully";
                response.ResponseCode=200;
                response.Data = userReturnDto;
            }
            else
            {
                response.IsSuccessful = false;
                response.ResponseCode= 404;
                
            }
            return response;
        }
    }
}
