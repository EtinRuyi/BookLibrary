using BookLibrary.Commons.Cloudinary;
using BookLibrary.Core.Services.Interface;
using BookLibrary.Data.Repository.Implementation;
using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using BookLibrary.Model.Entities.Shared;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BookLibrary.Core.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<BaseResponse<BookReturnDto>> AddBook(AddBookDTO addBookDTO)
        {
            var response = new BaseResponse<BookReturnDto>();
                       
            try
            {
                var bookExists = await _bookRepository.GetBookByTitleAndAuthor(addBookDTO.Title, addBookDTO.Author);
                if(bookExists != null)
                {
                    response.ResponseCode = 400;
                    response.Message = $"Book already exists";
                    response.IsSuccessful = false;
                    return response;
                }


                var book = new Book
                {
                    Title = addBookDTO.Title,
                    ISBN = addBookDTO.ISBN,
                    Author = addBookDTO.Author,
                    GenreId = addBookDTO.GenreId,
                    Description = addBookDTO.Description,
                    Content = addBookDTO.Content,
                    ImageUrl = addBookDTO.ImageUrl,
                    Publisher = addBookDTO.Publisher,
                    PublishedYear = addBookDTO.PublishedYear,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    
                };
                var bookAdded = await _bookRepository.AddBook(book);

                var returnAdded = new BookReturnDto
                {
                    Id = bookAdded.Id,
                    Title = bookAdded.Title,
                    ISBN = bookAdded.ISBN,
                    Author = bookAdded.Author,
                    GenreId = bookAdded.GenreId,
                    ImageUrl = bookAdded.ImageUrl,
                    Publisher = bookAdded.Publisher,
                    Description = bookAdded.Description,
                    Content = bookAdded.Content,
                    PublishedYear = bookAdded.PublishedYear,
                };
                response.ResponseCode = 200;
                response.Message = $"SuccessFully Added to create category";
                response.Data = returnAdded;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {

                response.ResponseCode = 500;
                response.Message = $"Failed to create category {ex.InnerException}";
                response.Data = null;

            }

            return response;


        }

        public async Task<BaseResponse<BookReturnDto>> DeleteBook(Guid id)
        {
            var response = new BaseResponse<BookReturnDto>();
            if (id == null)
            {
                response.Message = "Id cannot be equal to null";
            }
            var book = await _bookRepository.GetBookById(id);

            if (book != null)
            {
                await _bookRepository.DeleteBook(book);

                response.ResponseCode = 200;
                response.Message = "Book deleted successfully";
            }
            else
            {
                response.ResponseCode = 400;
                response.Message = "Book not found";
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<BookReturnDto>>> GetAllBook(int pagenumber, int pageSize)
        {
            int itemstoskip = (pagenumber - 1) * pageSize;
            var books = await _bookRepository.GetAllBooks(itemstoskip, pageSize);


            var response = new BaseResponse<IEnumerable<BookReturnDto>>();
            List<BookReturnDto> returnBooks = new List<BookReturnDto>();

            foreach (var book in books)
            {
                var returnAdded = new BookReturnDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Author = book.Author,
                    GenreId = book.GenreId,
                    ImageUrl = book.ImageUrl,
                    Publisher = book.Publisher,
                    Description = book.Description,
                    Content = book.Content,
                    PublishedYear = book.PublishedYear,
                };
                returnBooks.Add(returnAdded);
            }
            response.IsSuccessful = true;
            response.ResponseCode = 200;
            response.Message = "SuccessFully got list of books";
            response.Data = returnBooks;
            return response;
        }

        public async Task<BaseResponse<BookReturnDto>> GetBookById(Guid id)
        {
            var response = new BaseResponse<BookReturnDto>();

            var book = await _bookRepository.GetBookById(id);
            if (book != null)
            {
                var returnAdded = new BookReturnDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Author = book.Author,
                    GenreId = book.GenreId,
                    ImageUrl = book.ImageUrl,
                    Publisher = book.Publisher,
                    Description = book.Description,
                    Content = book.Content,
                    PublishedYear = book.PublishedYear,
                };

                response.ResponseCode = 200;
                response.Message = "SuccessFully got list of books";
                response.Data = returnAdded;
            }
            else
            {
                response.ResponseCode = 400;
                response.Message = "Book not found";
            }
            return response;
        }

        public async Task<BaseResponse<BookUpdateDto>> UpdateBook(BookUpdateDto book, Guid id)
        {
            var getbook = _bookRepository.GetBookById(id);
            var response = new BaseResponse<BookUpdateDto>();

            if (getbook != null)
            {
                var bookmodel = new Book
                {
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Author = book.Author,
                    GenreId = book.GenreId,
                    ImageUrl = book.ImageUrl,
                    Publisher = book.Publisher,
                    Description = book.Description,
                    Content = book.Content,
                    PublishedYear = book.PublishedYear
                };
                var updatedbook = await _bookRepository.UpdateBook(bookmodel);

                var returnBookDto = new BookUpdateDto
                {
                    //Id = updatedbook.Id,
                    Title = updatedbook.Title,
                    Author = updatedbook.Author,
                    GenreId = updatedbook.GenreId,
                    ImageUrl = updatedbook.ImageUrl,
                    Publisher = updatedbook.Publisher,
                    Description = updatedbook.Description,
                    Content = updatedbook.Content,
                    PublishedYear = updatedbook.PublishedYear,                
                    ISBN = updatedbook.ISBN
                    
                };

                response.ResponseCode = 200;
                response.IsSuccessful = true;
                response.Data = returnBookDto;
                response.Message = "Book updated successful";
            }
            else
            {
                response.ResponseCode = 400;
                response.IsSuccessful = false;
                response.Message = "Book not found";
            }
            return response;
        }

        public async Task<BaseResponse<bool>> SearchBookByTitleAndAuthor(string title, string author)
        {
            var response = new BaseResponse<bool>();
            
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {

                response.IsSuccessful = false;
                response.ResponseCode = 400;
                response.Message = "Invalid input";
            }
            else
            {
                var check = _bookRepository.GetBookByTitleAndAuthor(title, author);
                if(check != null)
                {
                    response.IsSuccessful = true;
                    response.Message = "Found successfully";
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message = "No book Found";
                }                
            }
            return response;
        }

        public async Task<Book> UpdatePhoto(Guid id, IFormFile image)
        {
            try
            {
                var book = await _bookRepository.GetBookById(id);
                if (book == null || image.Length <= 0)
                {
                    return null; // Return null to indicate that the book was not found.
                }

                var cloudinaryAcc = new Cloudinary(new Account(
                    CloudinarySettings.CloudName,
                    CloudinarySettings.Key,
                    CloudinarySettings.Secret
                ));

                var upload = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                };
                var uploadResult = await cloudinaryAcc.UploadAsync(upload);
                book.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;

                // Update the book in the database
                await _bookRepository.UpdateBook(book);

                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
