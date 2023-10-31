using BookLibrary.Data.DBContext;
using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        public BookRepository(BookLibraryDbContext dbContext) => _dbContext = dbContext;

        public async Task<Book> AddBook(Book book)
        {
             await _dbContext.Books.AddAsync(book);
            _dbContext.SaveChanges();
            return book;
        }


        public async Task<bool> DeleteBook(Book book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooks(int itemstoskip, int pageSize)
        {
            return await _dbContext.Books.Skip(itemstoskip).Take(pageSize).ToListAsync();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<Book> UpdateBook(Book book)
        {
            _dbContext.Update(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> GetBookByTitleAndAuthor(string title, string author)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(book => book.Title == title && book.Author == author);
        }

       

    }
}
