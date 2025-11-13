using System.Text.Json.Serialization;

namespace backend.models
{
    public class Review
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("LibroId")]
        public int LibroId { get; set; }

        [JsonPropertyName("UsuarioEmail")]
        public string UsuarioEmail { get; set; } = string.Empty;

        [JsonPropertyName("UsuarioNombre")]
        public string UsuarioNombre { get; set; } = string.Empty;

        [JsonPropertyName("Rating")]
        public int Rating { get; set; }

        [JsonPropertyName("Comentario")]
        public string Comentario { get; set; } = string.Empty;

    }
}
