using BookLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Data.Repository.Interface
{
    public interface IGenreRepository
    {
        Task<Genre> AddGenre(Genre genre);
        Task<Genre> UpdateGenre(Genre genre);
        Task<bool> DeleteGenre(Genre genre);
        Task<Genre> GetGenreById(Guid id);
        Task<IEnumerable<Genre>> GetAllGenre(int itemstoskip, int pageSize);
    }
}
