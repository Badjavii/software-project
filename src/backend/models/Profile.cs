using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace backend.models
{
    /**
     * @class Profile
     * @brief Clase abstracta que representa un perfil de usuario en el sistema Booksy.
     *
     * Contiene información personal del usuario, su historial de compras y métodos para gestionar dicha información.
     * Esta clase debe ser heredada por clases concretas como Cliente, Administrador, etc.
     */
    public abstract class Profile
    {
        // Atributos privados
        private string _email = null!;  //**@brief Correo electrónico del usuario.
        private string _firstName;      //**@brief Nombre del usuario.
        private string _lastName;       //**@brief Apellido del usuario.
        private int _age;               //**@brief Edad del usuario.
        private string _password;       //**@brief Contraseña del usuario. Solo modificable internamente.

        /**
         * @brief Constructor principal del perfil.
         * @param email Correo electrónico del usuario. Se valida que pertenezca al dominio ucab.edu.ve.
         * @param firstName Nombre del usuario.
         * @param lastName Apellido del usuario.
         * @param age Edad del usuario.
         * @param password Contraseña del usuario.
         */
        public Profile(string email, string firstName, string lastName, int age, string password)
        {
            Email = email;
            _firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            _lastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            _age = age;
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        // Getters y Setters

        //**@brief Obtiene o establece el correo electrónico del usuario. Solo acepta dominios ucab.edu.ve.
        [JsonPropertyName("_email")]
        public string Email
        {
            get => _email;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                // Regex para validar dominio ucab.edu.ve (incluye subdominios como est.ucab.edu.ve)
                var regex = new Regex(@"^[^@\s]+@([a-z]+\.)?ucab\.edu\.ve$", RegexOptions.IgnoreCase);

                if (!regex.IsMatch(value))
                    throw new ArgumentException("El correo debe pertenecer al dominio ucab.edu.ve");

                _email = value;
            }
        }

        //**@brief Obtiene o establece el nombre del usuario.
        [JsonPropertyName("_firstName")]
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value ?? throw new ArgumentNullException(nameof(value));
        }

        //**@brief Obtiene o establece el apellido del usuario.
        [JsonPropertyName("_lastName")]
        public string LastName
        {
            get => _lastName;
            set => _lastName = value ?? throw new ArgumentNullException(nameof(value));
        }

        //**@brief Obtiene o establece la edad del usuario.
        [JsonPropertyName("_age")]
        public int Age
        {
            get => _age;
            set => _age = value;
        }

        //**@brief Obtiene o establece la contraseña del usuario, pero desde el mismo objeto.
        [JsonPropertyName("_password")]
        private string Password
        {
            get => _password;
            set => _password = value ?? throw new ArgumentNullException(nameof(value));
        }

        // Metodos extras

        //**@brief Verifica que la contraseña sea correcta.
        public Boolean verifyPassword(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            return Password == password;
        }

        //**@brief Cambia la contraseña, solo si la actual es correcta.
        public void changePassword(string actualPassword, string newPassword)
        {
            if (actualPassword == null) throw new ArgumentNullException(nameof(actualPassword));
            if (newPassword == null) throw new ArgumentNullException(nameof(newPassword));

            if (verifyPassword(actualPassword) == false)
                throw new UnauthorizedAccessException("La contraseña actual no coincide.");

            Password = newPassword;
        }
    }
}
