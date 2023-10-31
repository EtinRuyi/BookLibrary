using Azure;
using BookLibrary.Core.Services.Interface;
using BookLibrary.Data.Repository.Implementation;
using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using BookLibrary.Model.Entities.Shared;
using System.Drawing.Printing;

namespace BookLibrary.Core.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<BaseResponse<IEnumerable<GenreReturnDto>>> GetAllGenre(int pagenumber, int pageSize)
        {
            int itemstoskip = (pagenumber - 1) * pageSize;
            var gottengenres = await _genreRepository.GetAllGenre(itemstoskip, pageSize);

            var response = new BaseResponse<IEnumerable<GenreReturnDto>>();
            var listOfGenres = new List<GenreReturnDto>();
            try
            {
                foreach (var genre in gottengenres)
                {
                    listOfGenres.Add(new GenreReturnDto
                    {
                        Id = genre.Id,
                        GenreName = genre.GenreName
                    });
                }
                response.Message = "All genres gotten successfully";
                response.Data = listOfGenres;
                response.IsSuccessful = true;
                response.ResponseCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "error:" + ex.InnerException;
                response.IsSuccessful = false;
                response.ResponseCode = 500;
                return response;
            }
        }
        public async Task<BaseResponse<GenreReturnDto>> GetGenreById(Guid id)
        {
            var response = new BaseResponse<GenreReturnDto>();

            try
            {
                var genre = await _genreRepository.GetGenreById(id);

                if (genre != null)
                {
                    var genreReturnDto = new GenreReturnDto
                    {
                        Id = genre.Id,
                        GenreName = genre.GenreName
                    };
                    response.Message = "Genre found successfully";
                    response.Data = genreReturnDto;
                    response.IsSuccessful = true;
                    response.ResponseCode = 200;
                }
                else
                {
                    response.Message = "Genre not found";
                    response.IsSuccessful = false;
                    response.ResponseCode = 404;

                }
                return response;

            }
            catch (Exception ex)
            {

                response.Message = "error:" + ex.InnerException;
                response.IsSuccessful = false;
                response.ResponseCode = 404;
                return response;
            }
        }

        public async Task<BaseResponse<GenreReturnDto>> AddGenre(GenreAddDto genreAddDto)
        {
            var response = new BaseResponse<GenreReturnDto>();
            var genre = new Genre
            {
                GenreName = genreAddDto.GenreName,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            try
            {
                var returngenre = await _genreRepository.AddGenre(genre);

                var returned = new GenreReturnDto
                {
                    Id = returngenre.Id,
                    GenreName = returngenre.GenreName,
                };
                response.ResponseCode = 200;
                response.Message = "Genre added successfully";
                response.Data = returned;
                response.IsSuccessful = true;
            }
            catch (Exception e)
            {
                response.ResponseCode = 500;
                response.Message = "Genre add failed. " + e.InnerException;
                response.IsSuccessful = false;
            }
            return response;
        }

        public async Task<BaseResponse<GenreReturnDto>> UpdateGenre(GenreAddDto genreAddDto, Guid id)
        {
            var response = new BaseResponse<GenreReturnDto>();
            
            try
            {
                var gottenGenre = await _genreRepository.GetGenreById(id);
                if (gottenGenre != null)
                {
                    gottenGenre.GenreName = genreAddDto.GenreName;
                    gottenGenre.DateUpdated = DateTime.Now;

                    var returngenre = await _genreRepository.UpdateGenre(gottenGenre);
                    var returned = new GenreReturnDto
                    {
                        Id= returngenre.Id,
                        GenreName= returngenre.GenreName                        

                    };
                    response.ResponseCode = 200;
                    response.Message = "Genre updated successfully";
                    response.Data = returned;
                    response.IsSuccessful = true;
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Message = "Genre id does not exist";
                    response.IsSuccessful = false;
                }

                
            }
            catch (Exception e)
            {
                response.ResponseCode = 500;
                response.Message = "Genre update failed. " + e.InnerException;
                response.IsSuccessful = false;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> DeleteGenre(Guid id)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var genre = await _genreRepository.GetGenreById(id);
                if (genre != null)
                {
                    if (await _genreRepository.DeleteGenre(genre))
                    {
                        response.Message = "Genre deleted successfully";
                        response.IsSuccessful = true;
                        response.Data = true;
                    }
                }
                else
                {
                    response.Message = "Genre id not found";
                    response.IsSuccessful = false;
                    response.Data = false;

                }
            }
            catch (Exception ex)
            {
                response.Message = "Genre delete failed ";
                response.IsSuccessful = false;
                response.Data = false;
                response.Message = "Some error occurred: " + ex.InnerException;
            }
            return response;
        }


    }
}

