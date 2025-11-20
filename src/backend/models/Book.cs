using System.Text.Json.Serialization;

namespace backend.models
{
    /**
     * @class Book
     * @brief Contiene la información de un libro en Booksy.
     */
    public class Book
    {
        //**@brief # Atributos del libro (inicializados en nulo).

        [JsonPropertyName("_id")]
        public int Id { get; set; }

        [JsonPropertyName("_nameBook")]
        public string NameBook { get; set; } = string.Empty;

        [JsonPropertyName("_subtitle")]
        public string Subtitle { get; set; } = string.Empty;

        [JsonPropertyName("_series")]
        public string Series { get; set; } = string.Empty;

        [JsonPropertyName("_author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("_language")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("_publisher")]
        public string Publisher { get; set; } = string.Empty;

        [JsonPropertyName("_bookCover")]
        public string BookCover { get; set; } = string.Empty;

        [JsonPropertyName("_typeBook")]
        public string TypeBook { get; set; } = string.Empty;

        [JsonPropertyName("_bookVolume")]
        public string BookVolume { get; set; } = string.Empty;

        [JsonPropertyName("_bookHeight")]
        public float BookHeight { get; set; }

        [JsonPropertyName("_bookWidth")]
        public float BookWidth { get; set; }

        [JsonPropertyName("_categoryList")]
        public List<string> CategoryList { get; set; } = new List<string>();

        [JsonPropertyName("_numPages")]
        public int NumPages { get; set; }

        [JsonPropertyName("_publishYear")]
        public DateTime PublishYear { get; set; } = DateTime.Today;

        [JsonPropertyName("_cost")]
        public float Cost { get; set; }

        [JsonPropertyName("_description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("_buyerEmail")]
        public string? BuyerEmail { get; set; } = null;

        [JsonPropertyName("_sellerEmail")]
        public string SellerEmail { get; set; } = string.Empty;

        //**@brief # Constructores.

        //**@brief Constructor vacio.
        public Book() { }

        //**@brief Constructor basico.
        public Book(int id, string nameBook, string subtitle, string series, string author, string language, string publisher, string bookCover, string typeBook, string bookVolume, float bookHeight, float bookWidth, List<string> categoryList, int numPages, DateTime publishYear, float cost, string description, string buyerEmail, string sellerEmail)
        {
            Id = id;
            NameBook = nameBook;
            Subtitle = subtitle;
            Series = series;
            Author = author;
            Language = language;
            Publisher = publisher;
            BookCover = bookCover;
            TypeBook = typeBook;
            BookVolume = bookVolume;
            BookHeight = bookHeight;
            BookWidth = bookWidth;
            CategoryList = categoryList;
            NumPages = numPages;
            PublishYear = publishYear;
            Cost = cost;
            Description = description;
            BuyerEmail = buyerEmail;
            SellerEmail = sellerEmail;
        }

        public bool ValidateNameBook()
        {
            string pattern = @"^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\s\:\-\,\.\'\(\)]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(NameBook, pattern);
        }
    }
}
