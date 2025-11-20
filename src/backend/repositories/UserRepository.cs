using System.Text.Json;
using backend.models;
using System.Reflection;

namespace backend.repositories
{
    /**
     * @class UserRepository
     * @brief Repositorio que gestiona la colección de perfiles de usuario en Booksy.
     *
     * Implementa el patrón Singleton. Carga los datos desde un archivo JSON al iniciar
     * y permite agregar, eliminar, buscar y exportar usuarios.
     */
    public class UserRepository
    {
        // Instancia única del repositorio (Singleton).
        private static readonly UserRepository _instance = new UserRepository();

        // Ruta del archivo JSON que contiene los datos de los usuarios.
        private readonly string _jsonPath = @"data/user.json";

        // Lista interna de perfiles cargados desde el archivo.
        private List<User> _users;

        // Dependencia: repositorio de libros para inicializar catálogos de vendedores.
        private readonly BookRepository _bookRepository = BookRepository.Instance;

        // Constructor privado. Llama al método Load() para inicializar la lista.
        private UserRepository()
        {
            _users = new List<User>();
            Load();
        }

        // Obtiene la instancia única del repositorio.
        public static UserRepository Instance => _instance;

        // Carga los datos de usuarios desde el archivo JSON.
        private void Load()
        {
            if (!File.Exists(_jsonPath)) return;

            string json = File.ReadAllText(_jsonPath);
            var rawUsers = JsonSerializer.Deserialize<List<JsonElement>>(json);

            if (rawUsers == null) return;

            foreach (var element in rawUsers)
            {
                string email = element.GetProperty("_email").GetString()!;
                string firstName = element.GetProperty("_firstName").GetString()!;
                string lastName = element.GetProperty("_lastName").GetString()!;
                int age = element.GetProperty("_age").GetInt32();
                string password = element.GetProperty("_password").GetString()!;
                bool isSeller = element.GetProperty("_isSeller").GetBoolean();

                User user = new User(email, firstName, lastName, age, password);
                user.PurchaseHistory = _bookRepository.GetPurchaseHistoryByBuyerEmail(email);

                if (isSeller)
                {
                    // Inicializar SellerInfo usando el repositorio de libros
                    var catalog = _bookRepository.GetCatalogBySellerEmail(email);

                    // Verificar si _sellerInfo existe y no es null
                    if (element.TryGetProperty("_sellerInfo", out JsonElement sellerInfoElement) &&
                        sellerInfoElement.ValueKind != JsonValueKind.Null)
                    {
                        string bankName = sellerInfoElement.GetProperty("_bankName").GetString() ?? "";
                        string accountNumber = sellerInfoElement.GetProperty("_accountNumber").GetString() ?? "";
                        string phoneNumber = sellerInfoElement.GetProperty("_phoneNumber").GetString() ?? "";

                        // Ratings
                        List<float> ratings = new();
                        if (sellerInfoElement.TryGetProperty("_ratings", out JsonElement ratingsElement) &&
                            ratingsElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var rating in ratingsElement.EnumerateArray())
                            {
                                ratings.Add(rating.GetSingle());
                            }
                        }

                        var sellerInfo = new SellerInfo(bankName, accountNumber, phoneNumber, catalog, ratings);
                        user.ActivateSellerRole(sellerInfo);
                    }
                }

                _users.Add(user);
            }
        }

        // Guarda todos los usuarios en el archivo JSON, sobrescribiendo el contenido.
        public void Save()
        {
            // Antes de guardar usuarios, también guardamos el estado del repositorio de libros
            _bookRepository.Save();

            // Creamos una lista de objetos anónimos que reflejen el formato esperado en user.json
            var exportUsers = _users.Select(u => new
            {
                _email = u.Email,
                _firstName = u.FirstName,
                _lastName = u.LastName,
                _age = u.Age,
                _password = u.Password,
                _isSeller = u.IsSeller,
                _sellerInfo = u.IsSeller && u.SellerInfo != null ? new
                {
                    _bankName = u.SellerInfo.BankName,
                    _accountNumber = u.SellerInfo.AccountNumber,
                    _phoneNumber = u.SellerInfo.PhoneNumber,
                    _ratings = u.SellerInfo.Ratings
                } : null
                // No incluimos PurchaseHistory porque se reconstruye desde BookRepository
            }).ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(exportUsers, options);

            File.WriteAllText(_jsonPath, json);
        }


        // Agregar un comprador (usuario normal).
        public void AddBuyer(User user)
        {
            _users.Add(user);
            Save();
        }

        // Agregar un vendedor: si existe, cambia rol; si no, lo crea.
        public void AddSeller(User user, SellerInfo info)
        {
            var existingUser = ReturnUserByEmail(user.Email);
            if (existingUser != null)
            {
                existingUser.ActivateSellerRole(info);
            }
            else
            {
                user.ActivateSellerRole(info);
                _users.Add(user);
                Save();
            }
        }

        // Elimina un usuario del repositorio.
        public void RemoveUser(User user)
        {
            _users.Remove(user);
            Save();
        }

        // Busca y retorna un usuario específico por nombre y apellido.
        public User? ReturnUser(string firstName, string lastName)
        {
            return _users.FirstOrDefault(u =>
                u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        // Busca y retorna un usuario por correo.
        public User? ReturnUserByEmail(string email)
        {
            return _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // Verifica si existe un usuario con un valor específico en cualquier atributo.
        public bool ExistsUser(string attribute, object value)
        {
            foreach (var user in _users)
            {
                PropertyInfo? prop = user.GetType().GetProperty(attribute);
                if (prop != null)
                {
                    var propValue = prop.GetValue(user);
                    if (propValue != null && propValue.Equals(value))
                        return true;
                }
            }
            return false;
        }

        // Devuelve la lista completa de usuarios.
        public List<User> ReturnUsers()
        {
            return _users;
        }
    }
}
