using BookLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Data.Repository.Interface
{
    public interface IBookRepository
    {
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<bool> DeleteBook(Book book);
        Task<IEnumerable<Book>> GetAllBooks(int itemstoskip, int pageSize);
        Task<Book> GetBookById(Guid id);
        Task<Book> GetBookByTitleAndAuthor(string title, string author);

    }
}
