using BookLibrary.Data.DBContext;
using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Repository.Implementation
{
    public class GenreRepository : IGenreRepository
    {

        private readonly BookLibraryDbContext _bookLibraryDbContext;

        public GenreRepository(BookLibraryDbContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }
        public async Task<Genre> AddGenre(Genre genre)
        {
            _bookLibraryDbContext.Genre.Add(genre);
            await _bookLibraryDbContext.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> DeleteGenre(Genre genre)
        {
            _bookLibraryDbContext.Genre.Remove(genre);
            await _bookLibraryDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await _bookLibraryDbContext.Genre.ToListAsync();
        }
        public async Task<IEnumerable<Genre>> GetAllGenre(int itemstoskip, int pageSize)
        {
            return await _bookLibraryDbContext.Genre.Skip(itemstoskip).Take(pageSize).ToListAsync();
        }
        public async Task<Genre> GetGenreById(Guid id)
        {
            return await _bookLibraryDbContext.Genre.FindAsync(id);
        }

        public async Task<Genre> UpdateGenre(Genre genre)
        {
             _bookLibraryDbContext.Update(genre);
            await _bookLibraryDbContext.SaveChangesAsync();
            return genre;
        }
    }
}
