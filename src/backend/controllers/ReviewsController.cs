using backend.dtos;
using backend.services;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService _service = new();

        [HttpPost]
        public IActionResult Create([FromBody] CreateReviewDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.BookExists(dto.LibroId))
            {
                return NotFound(new { message = "Libro no encontrado" });
            }

            if (dto.Comentario.Length > 100)
            {
                return BadRequest(new { message = "La reseña debe tener máximo 100 caracteres" });
            }

            var review = _service.CreateReview(dto);
            return CreatedAtAction(nameof(GetByBook), new { libroId = dto.LibroId }, review);
        }

        [HttpGet]
        public IActionResult GetByBook([FromQuery] int libroId)
        {
            if (libroId <= 0)
            {
                return BadRequest(new { message = "libroId es requerido" });
            }

            var reviews = _service.GetByBook(libroId);
            return Ok(reviews);
        }

        [HttpGet("mine")]
        public IActionResult GetMyReviews([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { message = "email es requerido" });
            }

            var reviews = _service.GetByUser(email);
            return Ok(reviews);
        }
    }
}
