using System.Text.Json;
using System.Text.Json.Serialization;
using backend.models;
using System.Reflection;

namespace backend.repositories
{
    /**
     * @class BookRepository
     * @brief Repositorio que gestiona la colección de libros en Booksy.
     *
     * Implementa el patrón Singleton. Carga los datos desde un archivo JSON al iniciar
     * y permite agregar, eliminar, buscar y exportar libros.
     */
    public class BookRepository
    {
        // Instancia única del repositorio (Singleton).
        private static readonly BookRepository _instance = new BookRepository();

        // Ruta del archivo JSON que contiene los datos de los libros.
        private readonly string _jsonPath = @"data/book.json";

        // Lista interna de libros cargados desde el archivo.
        private List<Book> _books;

        // Constructor privado. Llama al método Load() para inicializar la lista.
        private BookRepository()
        {
            _books = new List<Book>();
            Load();
        }

        // Obtiene la instancia única del repositorio.
        public static BookRepository Instance => _instance;

        /**
         * @brief Carga los datos de libros desde el archivo JSON.
         *
         * Lee cada objeto, extrae sus atributos y crea instancias de Book.
         */
        private void Load()
        {
            if (!File.Exists(_jsonPath)) return;

            string json = File.ReadAllText(_jsonPath);
            var rawBooks = JsonSerializer.Deserialize<List<JsonElement>>(json);

            if (rawBooks == null) return;

            foreach (var element in rawBooks)
            {
                int id = element.GetProperty("_id").GetInt32();
                string nameBook = element.GetProperty("_nameBook").GetString() ?? string.Empty;
                string subtitle = element.GetProperty("_subtitle").GetString() ?? string.Empty;
                string series = element.GetProperty("_series").GetString() ?? string.Empty;
                string author = element.GetProperty("_author").GetString() ?? string.Empty;
                string language = element.GetProperty("_language").GetString() ?? string.Empty;
                string publisher = element.GetProperty("_publisher").GetString() ?? string.Empty;
                string bookCover = element.GetProperty("_bookCover").GetString() ?? string.Empty;
                string typeBook = element.GetProperty("_typeBook").GetString() ?? string.Empty;
                string bookVolume = element.GetProperty("_bookVolume").GetString() ?? string.Empty;
                float bookHeight = element.GetProperty("_bookHeight").GetSingle();
                float bookWidth = element.GetProperty("_bookWidth").GetSingle();
                var categoryList = element.GetProperty("_categoryList").EnumerateArray().Select(c => c.GetString() ?? "").ToList();
                int numPages = element.GetProperty("_numPages").GetInt32();
                DateTime publishYear = element.GetProperty("_publishYear").GetDateTime();
                float cost = element.GetProperty("_cost").GetSingle();
                string description = element.GetProperty("_description").GetString() ?? string.Empty;
                string buyerEmail = element.GetProperty("_buyerEmail").GetString() ?? string.Empty;
                string sellerEmail = element.GetProperty("_sellerEmail").GetString() ?? string.Empty;

                Book book = new Book(id, nameBook, subtitle, series, author, language, publisher, bookCover, typeBook, bookVolume, bookHeight, bookWidth, categoryList, numPages, publishYear, cost, description, buyerEmail, sellerEmail);
                _books.Add(book);
            }
        }

        // Guarda todos los libros en el archivo JSON, sobrescribiendo el contenido.
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_books, options);
            File.WriteAllText(_jsonPath, json);
        }

        // Agrega un nuevo libro al repositorio con ID autoincremental.
        public void AddBook(Book book)
        {
            int newId = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
            book.Id = newId;
            _books.Add(book);
            Save();
        }

        // Elimina un libro del repositorio usando su ID.
        public bool RemoveBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
                Save();
                return true;
            }
            return false;
        }

        // Busca y retorna un libro específico por ID.
        public Book? SearchBook(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        // Devuelve el catálogo de un vendedor específico (por correo).
        public List<Book> GetCatalogBySellerEmail(string sellerEmail)
        {
            return _books.Where(b => b.SellerEmail.Equals(sellerEmail, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> GetPurchaseHistoryByBuyerEmail(string buyerEmail)
        {
            return _books.Where(b => b.BuyerEmail != null && b.BuyerEmail.Equals(buyerEmail, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        // Devuelve la lista completa de libros.
        public List<Book> ReturnBooks()
        {
            return _books;
        }
    }
}
