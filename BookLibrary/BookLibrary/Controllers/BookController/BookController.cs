using BookLibrary.Core.Services.Interface;
using BookLibrary.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers.BookController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookId(Guid id)
        {
            return Ok(await _bookService.GetBookById(id));
        }

        [HttpGet]
        [Route("AllBooks")]
        public async Task<IActionResult> GetAllBooks(int pagenumber = 1, int pagesize = 10)
        {
            var getAll = await _bookService.GetAllBook(pagenumber, pagesize);
            return Ok(getAll);
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddNewBook(AddBookDTO addBookDTO)
        {
            var returnbook = await _bookService.AddBook(addBookDTO);
            return Ok(returnbook);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(BookReturnDto bookReturnDto, Guid id)
        {
            return Ok(await _bookService.UpdateBook(bookReturnDto, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _bookService.DeleteBook(id));
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SerachByTitleAndAuthor(string title, string author)
        {
            return Ok(await _bookService.SearchBookByTitleAndAuthor(title, author));
        }

        [HttpPatch("ImageUpload/{id}")]
        public async Task<IActionResult> UploadPhoto(Guid id, IFormFile image)
        {
            var response = await _bookService.UpdatePhoto(id, image);
            var repAdd = "";
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound("Book not found or image not provided.");
            }
        }
    }
}
