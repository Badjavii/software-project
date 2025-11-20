using System.Text.Json.Serialization;
using System.Collections.Generic;
using backend.models;

namespace backend.models
{
    /**
     * @class User
     * @brief Representa un perfil único de usuario en Booksy, que puede ser comprador y/o vendedor.
     */
    public class User
    {
        //**@brief # Datos personales con getters y setters.

        [JsonPropertyName("_email")]        //**@brief Correo del usuario.
        public string Email { get; set; }

        [JsonPropertyName("_firstName")]    //**@brief Nombre del usuario.
        public string FirstName { get; set; }

        [JsonPropertyName("_lastName")]     //**@brief Apellido del usuario.
        public string LastName { get; set; }

        [JsonPropertyName("_age")]          //**@brief Edad del usuario.
        public int Age { get; set; }

        [JsonPropertyName("_password")]     //**@brief Contraseña del usuario.
        public string Password { get; set; }

        [JsonPropertyName("_isSeller")]     //**@brief Rol del usuario.
        public bool IsSeller { get; set; }

        [JsonPropertyName("_sellerInfo")]   //**@brief Informacion del vendedor.
        public SellerInfo? SellerInfo { get; set; }

        [JsonPropertyName("_purchaseHistory")]
        public List<Book> PurchaseHistory { get; set; } = new List<Book>();

        //**@brief # Constructores.

        //**@brief Constructor basico.
        public User(string email, string firstName, string lastName, int age, string password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Age = age;
            Password = password ?? throw new ArgumentNullException(nameof(password));
            IsSeller = false;
            SellerInfo = null;
            PurchaseHistory = new List<Book>();
        }

        //**@brief # Metodos.

        //**@brief Verificar contraseña.
        public bool VerifyPassword(string password)
        {
            return Password == password;
        }

        //**@brief Cambiar contraseña.
        public void ChangePassword(string actualPassword, string newPassword)
        {
            if (!VerifyPassword(actualPassword))
                throw new UnauthorizedAccessException("La contraseña actual no coincide.");

            Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        }

        //**@brief Activar rol de vendedor.
        public void ActivateSellerRole(SellerInfo info)
        {
            IsSeller = true;
            SellerInfo = info;
        }
    }
}
