using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;

namespace backend.controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;

        public BookController()
        {
            _service = new BookService();
        }

        [HttpPost("register")]
        public IActionResult RegisterBook([FromBody] Book book)
        {
            try
            {
                _service.RegisterBook(book);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get")]
        public IActionResult GetBookByName([FromQuery] string name)
        {
            var book = _service.GetBookByName(name);
            if (book == null)
                return NotFound(new { error = "Libro no encontrado." });

            return Ok(book);
        }

        [HttpGet("seller")]
        public IActionResult GetBooksBySeller([FromQuery] string sellerEmail)
        {
            return Ok(_service.GetBooksBySeller(sellerEmail));
        }

        [HttpGet("author")]
        public IActionResult GetBooksByAuthor([FromQuery] string author)
        {
            return Ok(_service.GetBooksByAuthor(author));
        }

        [HttpGet("category")]
        public IActionResult GetBooksByCategory([FromQuery] string category)
        {
            return Ok(_service.GetBooksByCategory(category));
        }

        [HttpGet("exists")]
        public IActionResult BookExists([FromQuery] string name)
        {
            bool exists = _service.BookExists(name);
            return Ok(new { exists });
        }

        [HttpGet("all")]
        public IActionResult GetAllBooks()
        {
            return Ok(_service.GetAllBooks());
        }
    }
}
