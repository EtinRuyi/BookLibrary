using BookLibrary.Core.Services.Interface;
using BookLibrary.Data.Repository.Interface;
using BookLibrary.Model.DTOs;
using BookLibrary.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers.GenreController
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {

        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {

            _genreService = genreService;
        }

        [HttpGet]
        [Route("AllGenres")]
        public async Task<IActionResult> GetAllGenre(int pagenumber=1, int pagesize = 10)
        {
            var allGenres = await _genreService.GetAllGenre(pagenumber,pagesize);
            return Ok(allGenres);
        }

        [HttpPost]
        [Route("AddGenre")]
        public async Task<IActionResult> AddGenre(GenreAddDto genreAddDto)
        {
            return Ok(await _genreService.AddGenre(genreAddDto));
        }

        [HttpPut]
        [Route("UpdateGenre")]
        public async Task<IActionResult> UpdateGenre(GenreAddDto genreAddDto, Guid id)
        {
            return Ok(await _genreService.UpdateGenre(genreAddDto,id));
        }

        [HttpDelete]
        [Route("DeleteGenre")]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            return Ok( await _genreService.DeleteGenre(id));
        }

        [HttpGet]
        [Route("GetGenreById")]
        public async Task<IActionResult> GetGenreById(Guid id)
        {
            return Ok( await _genreService.GetGenreById(id));
        }
    }
}
