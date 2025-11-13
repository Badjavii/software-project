namespace backend.models
{
    /**
     * @class Seller
     * @brief Representa un usuario vendedor en el sistema Booksy.
     *
     * Hereda de la clase abstracta Profile y añade funcionalidades específicas para vendedores,
     * como un catálogo de libros en venta y una lista de calificaciones recibidas.
     */
    public class Seller : Profile
    {
        // Atributos privados
        private List<Book> _catalog;    //**@brief Lista de libros que el vendedor tiene en su catálogo.
        private List<float> _ratings;   //**@brief Lista de calificaciones recibidas por el vendedor.

        /**
         * @brief Constructor del vendedor.
         *
         * Inicializa los atributos personales heredados de Profile y crea listas vacías para el catálogo y las calificaciones.
         *
         * @param email Correo electrónico del vendedor.
         * @param firstName Nombre del vendedor.
         * @param lastName Apellido del vendedor.
         * @param age Edad del vendedor.
         * @param password Contraseña del vendedor.
         */
        public Seller(string email, string firstName, string lastName, int age, string password)
            : base(email, firstName, lastName, age, password)
        {
            _catalog = new List<Book>();
            _ratings = new List<float>();
        }
        
        public Seller()
            : base("", "", "", 0, "")
        {
            _catalog = new List<Book>();
            _ratings = new List<float>();
        }

        //**@brief Obtiene o establece el catálogo de libros del vendedor.
        public List<Book> Catalog
        {
            get => _catalog;
            set => _catalog = value ?? new List<Book>();
        }

        //**@brief Obtiene o establece la lista de calificaciones del vendedor.
        public List<float> Ratings
        {
            get => _ratings;
            set => _ratings = value ?? new List<float>();
        }

        /**
         * @brief Agrega un libro al catálogo del vendedor.
         * @param book Objeto Book que representa el libro a agregar.
         */
        public void AddBook(Book book)
        {
            _catalog.Add(book);
        }
    }
}
