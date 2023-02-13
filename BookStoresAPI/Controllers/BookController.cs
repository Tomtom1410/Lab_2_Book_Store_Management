using BookStoreClient.Models;
using BookStoresAPI.DataAccess.Interface;
using BookStoresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BookStoresAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ODataController
    {
        private readonly IBookRepositories _bookRepositories;

        public BookController(IBookRepositories bookRepositories)
        {
            _bookRepositories = bookRepositories;
        }

        [HttpGet]
        [Route("GetAll")]
        [EnableQuery]
        public async Task<IActionResult> GetAllBook()
        {
            var books = await _bookRepositories.GetAllBook();
            return Ok(books);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] BookDto bookDto)
        {
            var book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                IBSN = bookDto.IBSN,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Price = bookDto.Price,
                City = bookDto.City,
                Street = bookDto.Street,
            };
            var result = await _bookRepositories.CreateAsync(book);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] BookDto bookDto)
        {
            var book = new Book
            {
                Id = bookDto.Id,
                IBSN = bookDto.IBSN,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Price = bookDto.Price,
                City = bookDto.City,
                Street = bookDto.Street,
            };
            var result = await _bookRepositories.UpdateAsync(book);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _bookRepositories.DeleteAsync(id);
            return Ok(result);
        }
    }
}
