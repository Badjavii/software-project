using System.Text.Json.Serialization;
using backend.models;

namespace backend.models
{
    /**
     * @class SellerInfo
     * @brief Contiene la información específica del rol vendedor.
     */
    public class SellerInfo
    {
        //**@brief # Datos personales de vendedores con getters y setters.


        [JsonPropertyName("_bankName")]     //**@brief Nombre del banco del vendedor.
        public string BankName { get; set; }

        [JsonPropertyName("_accountNumber")]//**@brief Número de cuenta del vendedor.
        public string AccountNumber { get; set; }

        [JsonPropertyName("_phoneNumber")]  //**@brief Número de cuenta del vendedor.
        public string PhoneNumber { get; set; }

        [JsonPropertyName("_ratings")]   //**@brief Catálogo de libros del vendedor con sus calificaciones.
        public List<float> Ratings { get; set; }

        [JsonPropertyName("_catalog")]   //**@brief Catálogo de libros del vendedor con sus calificaciones.
        public List<Book> Catalog { get; set; }


        //**@brief # Constructores.

        // Constructor basico para nuevos vendedores.
        public SellerInfo(string bankName, string accountNumber, string phoneNumber)
        {
            BankName = bankName ?? throw new ArgumentNullException(nameof(bankName));
            AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Catalog = new List<Book>();
            Ratings = new List<float>();
        }

        // Reutilizar constructor basico para crear un nuevo constructor para la lectura de archivos
        public SellerInfo(string bankName, string accountNumber, string phoneNumber, List<Book> catalog, List<float> ratings)
        {
            BankName = bankName ?? throw new ArgumentNullException(nameof(bankName));
            AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Catalog = catalog ?? new List<Book>();
            Ratings = ratings ?? new List<float>();
        }


        //**@brief # Metodos.

        //**@brief Añadir un nuevo libro con su calificacion.
        public void AddRatedBook(Book book, float rating)
        {
            Ratings.Add(rating);
            Catalog.Add(book);
        }

    }
}