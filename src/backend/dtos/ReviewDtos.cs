using System.ComponentModel.DataAnnotations;

namespace backend.dtos
{
    public class CreateReviewDto
    {
        [Required]
        public int LibroId { get; set; }

        [Required]
        [EmailAddress]
        public string UsuarioEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(60)]
        public string UsuarioNombre { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(100)]
        public string Comentario { get; set; } = string.Empty;
    }

    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
