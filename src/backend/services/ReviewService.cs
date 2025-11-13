using System.Text.Json;
using backend.dtos;
using backend.models;

namespace backend.services
{
    public class ReviewService
    {
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        private readonly string _dataDir;
        private readonly string _reviewsPath;
        private readonly string _booksPath;

        public ReviewService()
        {
            _dataDir = Path.Combine(AppContext.BaseDirectory, "data");
            Directory.CreateDirectory(_dataDir);
            _reviewsPath = Path.Combine(_dataDir, "reviews.json");
            _booksPath = Path.Combine(_dataDir, "book.json");
            if (!File.Exists(_reviewsPath))
            {
                File.WriteAllText(_reviewsPath, "[]");
            }
        }

        private List<Review> LoadReviews()
        {
            var json = File.ReadAllText(_reviewsPath);
            return JsonSerializer.Deserialize<List<Review>>(json, _options) ?? new List<Review>();
        }

        private void SaveReviews(IEnumerable<Review> reviews)
        {
            var json = JsonSerializer.Serialize(reviews, _options);
            File.WriteAllText(_reviewsPath, json);
        }

        public bool BookExists(int libroId)
        {
            var book = new Book();
            var books = book.LoadBooks(_booksPath);
            return books.Any(b => b.Id == libroId);
        }

        public ReviewResponseDto CreateReview(CreateReviewDto dto)
        {
            var reviews = LoadReviews();
            var newReview = new Review
            {
                Id = reviews.Any() ? reviews.Max(r => r.Id) + 1 : 1,
                LibroId = dto.LibroId,
                UsuarioEmail = dto.UsuarioEmail,
                UsuarioNombre = dto.UsuarioNombre,
                Rating = dto.Rating,
                Comentario = dto.Comentario.Trim()
            };

            reviews.Add(newReview);
            SaveReviews(reviews);

            return ToResponse(newReview);
        }

        public IEnumerable<ReviewResponseDto> GetByBook(int libroId)
        {
            return LoadReviews()
                .Where(r => r.LibroId == libroId)
                .Select(ToResponse);
        }

        public IEnumerable<ReviewResponseDto> GetByUser(string email)
        {
            return LoadReviews()
                .Where(r => r.UsuarioEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
                .Select(ToResponse);
        }

        private static ReviewResponseDto ToResponse(Review review) => new()
        {
            Id = review.Id,
            LibroId = review.LibroId,
            UsuarioEmail = review.UsuarioEmail,
            UsuarioNombre = review.UsuarioNombre,
            Rating = review.Rating,
            Comentario = review.Comentario,
        };
    }
}
