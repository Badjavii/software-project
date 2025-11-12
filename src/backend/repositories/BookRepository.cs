using System.Text.Json;
using System.Text.Json.Serialization;
using backend.models;
using System.Reflection;

namespace backend.repositories
{
    /**
     * @class BookRepository
     * @brief Repositorio que gestiona la colección de libros en el sistema Booksy.
     *
     * Implementa el patrón Singleton. Carga los datos desde un archivo JSON al iniciar
     * y permite agregar, buscar, validar y exportar libros.
     */
    public class BookRepository
    {
        //**@brief Instancia única del repositorio (Singleton).
        private static readonly BookRepository _instance = new BookRepository();

        //**@brief Ruta del archivo JSON que contiene los datos de los libros.
        private readonly string _jsonPath = @"data/books.json";

        //**@brief Lista interna de libros cargados desde el archivo.
        private List<Book> _books;

        //**@brief Constructor privado. Llama al método Load() para inicializar la lista.
        private BookRepository()
        {
            _books = new List<Book>();
            Load();
        }

        //**@brief Obtiene la instancia única del repositorio.
        public static BookRepository Instance => _instance;

        /**
         * @brief Carga los datos de libros desde el archivo JSON.
         *
         * Lee cada objeto, extrae sus atributos, crea instancias y las agrega a la lista.
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
                string nameBook = element.GetProperty("_nameBook").GetString()!;
                string subtitle = element.GetProperty("_subtitle").GetString()!;
                string series = element.GetProperty("_series").GetString()!;
                string author = element.GetProperty("_author").GetString()!;
                string language = element.GetProperty("_language").GetString()!;
                string publisher = element.GetProperty("_publisher").GetString()!;
                string bookCover = element.GetProperty("bookCover").GetString()!;
                string typeBook = element.GetProperty("_typeBook").GetString()!;
                string bookVolume = element.GetProperty("_bookVolume").GetString()!;
                float bookHeight = element.GetProperty("_bookHeight").GetSingle();
                float bookWidth = element.GetProperty("_bookWidth").GetSingle();
                int numPages = element.GetProperty("_numPages").GetInt32();
                int publishYear = element.GetProperty("_publishYear").GetInt32();
                float cost = element.GetProperty("_cost").GetSingle();
                string sellerEmail = element.GetProperty("_sellerEmail").GetString()!;

                var categoryList = new List<string>();
                foreach (var category in element.GetProperty("_categoryList").EnumerateArray())
                {
                    categoryList.Add(category.GetString()!);
                }

                Book book = new Book(id, nameBook, subtitle, series, author, language, publisher, bookCover, typeBook, bookVolume, bookHeight, bookWidth, categoryList, numPages, publishYear, cost, sellerEmail);
                _books.Add(book);
            }
        }

        /**
         * @brief Guarda todos los libros en el archivo JSON, sobrescribiendo el contenido.
         */
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_books, options);
            File.WriteAllText(_jsonPath, json);
        }

        //**@brief Agrega un nuevo libro al repositorio.
        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        //**@brief Busca un libro por su nombre exacto.
        public Book? FindByName(string name)
        {
            return _books.FirstOrDefault(b => b.NameBook.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        //**@brief Verifica si existe un libro con el nombre dado.
        public bool ExistsByName(string name)
        {
            return _books.Any(b => b.NameBook.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        //**@brief Retorna todos los libros vendidos por un vendedor específico.
        public List<Book> FindBySeller(string sellerEmail)
        {
            return _books.Where(b => b.SellerEmail.Equals(sellerEmail, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        //**@brief Retorna todos los libros escritos por un autor específico.
        public List<Book> FindByAuthor(string author)
        {
            return _books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        //**@brief Retorna la instancia completa de un libro por su nombre.
        public Book? GetBookInstance(string name)
        {
            return FindByName(name);
        }

        /**
         * @brief Verifica si existe un libro con un valor específico en cualquier atributo.
         *
         * @param attribute Nombre del atributo (por ejemplo: "Author", "Publisher").
         * @param value Valor a buscar.
         * @return true si existe al menos un libro con ese valor; false en caso contrario.
         */
        public bool ExistsBook(string attribute, object value)
        {
            foreach (var book in _books)
            {
                PropertyInfo? prop = book.GetType().GetProperty(attribute);
                if (prop != null)
                {
                    var propValue = prop.GetValue(book);
                    if (propValue != null && propValue.Equals(value))
                        return true;
                }
            }
            return false;
        }

        //**@brief Retorna todos los libros registrados.
        public List<Book> GetAllBooks()
        {
            return _books;
        }

        /**
         * @brief Retorna todos los libros que contienen una categoría específica.
         * @param category Nombre de la categoría.
         * @return Lista de libros que incluyen esa categoría.
         */
        public List<Book> FindByCategory(string category)
        {
            return _books
                .Where(b => b.CategoryList.Any(c => c.Equals(category, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}
