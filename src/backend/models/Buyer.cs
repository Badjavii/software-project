namespace backend.models
{
    /**
     * @class Buyer
     * @brief Representa un usuario comprador en el sistema Booksy.
     *
     * Hereda de la clase abstracta Profile y añade funcionalidades específicas para compradores,
     * como el historial de compras realizadas.
     */
    public class Buyer : Profile
    {
        // Atributos privados
        private List<BookPurchase> _purchaseHistory;    //**@brief Historial de compras realizadas por el comprador.

        /**
         * @brief Constructor del comprador.
         *
         * Inicializa los atributos personales heredados de Profile y crea una lista vacía para el historial de compras.
         *
         * @param email Correo electrónico del comprador.
         * @param firstName Nombre del comprador.
         * @param lastName Apellido del comprador.
         * @param age Edad del comprador.
         * @param password Contraseña del comprador.
         */
        public Buyer(string email, string firstName, string lastName, int age, string password)
            : base(email, firstName, lastName, age, password)
        {
            _purchaseHistory = new List<BookPurchase>();
        }

        //**@brief Obtiene el historial de compras del comprador.
        public List<BookPurchase> PurchaseHistory
        {
            get => _purchaseHistory;
        }

        /**
         * @brief Agrega una nueva compra al historial del comprador.
         * @param book Objeto Book que representa el libro comprado.
         */
        public void AddPurchase(Book book)
        {
            _purchaseHistory.Add(new BookPurchase(book, DateTime.Now));
        }
    }
}
