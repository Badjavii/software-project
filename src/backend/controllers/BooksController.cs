using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;

namespace backend.controllers
{
    /**
     * @class BooksController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con libros.
     *
     * Expone endpoints para consultar, agregar, eliminar y buscar libros.
     */
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _service = new BookService();

        /**
         * @brief Devuelve todos los libros.
         */
        [HttpGet("all")]
        public IActionResult GetBooks()
        {
            var books = _service.GetBooks();
            return Ok(books);
        }

        /**
         * @brief Agrega un nuevo libro.
         */
        [HttpPost("add")]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null) return BadRequest(new { error = "El libro no puede ser nulo." });

            var addedBook = _service.AddBook(book);
            return CreatedAtAction(nameof(GetBooks), new { id = addedBook.Id }, addedBook);
        }

        /**
         * @brief Elimina un libro por ID.
         */
        [HttpDelete("remove/{id}")]
        public IActionResult RemoveBook(int id)
        {
            bool removed = _service.RemoveBook(id);
            if (!removed)
                return NotFound(new { error = "Libro no encontrado." });

            return Ok(new { message = "Libro eliminado correctamente." });
        }

        /**
         * @brief Busca un libro por ID.
         */
        [HttpGet("search/{id}")]
        public IActionResult SearchBook(int id)
        {
            var book = _service.SearchBook(id);
            if (book == null)
                return NotFound(new { error = "Libro no encontrado." });

            return Ok(book);
        }

        /**
         * @brief Devuelve el catálogo de un vendedor.
         */
        [HttpGet("catalog")]
        public IActionResult GetCatalogBySeller([FromQuery] string sellerEmail)
        {
            var catalog = _service.GetCatalogBySeller(sellerEmail);
            if (catalog.Count == 0)
                return NotFound(new { error = "No se encontraron libros para este vendedor." });

            return Ok(catalog);
        }

        /**
         * @brief Devuelve el historial de compras de un comprador.
         */
        [HttpGet("purchase-history")]
        public IActionResult GetPurchaseHistory([FromQuery] string buyerEmail)
        {
            var history = _service.GetPurchaseHistory(buyerEmail);
            if (history.Count == 0)
                return NotFound(new { error = "No se encontraron compras para este usuario." });

            return Ok(history);
        }
    }
}
