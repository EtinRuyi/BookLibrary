using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Services.Interface
{
    public interface IGenreService
    {
        Task<BaseResponse<IEnumerable<GenreReturnDto>>> GetAllGenre(int pagenumber, int pageSize);
        Task<BaseResponse<GenreReturnDto>> AddGenre(GenreAddDto genreAddDto);
        Task<BaseResponse<GenreReturnDto>> UpdateGenre(GenreAddDto genreAddDto, Guid id);
        Task<BaseResponse<bool>> DeleteGenre(Guid id);
        Task<BaseResponse<GenreReturnDto>> GetGenreById(Guid id);
    }
}
