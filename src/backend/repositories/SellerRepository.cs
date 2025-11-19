using System.Text.Json;
using System.Text.Json.Serialization;
using backend.models;
using backend.factories;
using System.Reflection;
using ProfileFactory = backend.factories.ProfileFactory;

namespace backend.repositories
{
    /**
     * @class SellerRepository
     * @brief Repositorio que gestiona la colección de vendedores en el sistema Booksy.
     *
     * Implementa el patrón Singleton. Carga los datos desde un archivo JSON al iniciar
     * y permite agregar, eliminar, buscar y exportar vendedores.
     */
    public class SellerRepository
    {
        //**@brief Instancia única del repositorio (Singleton).
        private static readonly SellerRepository _instance = new SellerRepository();

        //**@brief Ruta del archivo JSON que contiene los datos de los vendedores.
        private readonly string _jsonPath = @"data/sellers.json";

        //**@brief Lista interna de vendedores cargados desde el archivo.
        private List<Seller> _sellersList;

        //**@brief Constructor privado. Llama al método Load() para inicializar la lista.
        private SellerRepository()
        {
            _sellersList = new List<Seller>();
            Load();
        }

        //**@brief Obtiene la instancia única del repositorio
        public static SellerRepository Instance => _instance;

        //**@brief Carga los datos de vendedores desde el archivo JSON.
        private void Load()
        {
            // No existe la ruta o el archivo
            if (!File.Exists(_jsonPath)) return;

            string json = File.ReadAllText(_jsonPath);
            var rawSellers = JsonSerializer.Deserialize<List<JsonElement>>(json);

            if (rawSellers == null) return;

            foreach (var element in rawSellers)
            {
                string email = element.GetProperty("_email").GetString()!;
                string firstName = element.GetProperty("_firstName").GetString()!;
                string lastName = element.GetProperty("_lastName").GetString()!;
                int age = element.GetProperty("_age").GetInt32()!;
                string password = element.GetProperty("_password").GetString()!;

                Seller seller = ProfileFactory.CreateSeller(email, firstName, lastName, age, password);
                _sellersList.Add(seller);
            }
        }

        //**@brief Guarda todos los vendedores en el archivo JSON, sobrescribiendo el contenido.
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_sellersList, options);
            File.WriteAllText(_jsonPath, json);
        }

        //**@brief Agrega un nuevo vendedor al repositorio.
        public void AddSeller(Seller seller)
        {
            _sellersList.Add(seller);
        }

        //**@brief Elimina un vendedor del repositorio.
        public void RemoveSeller(Seller seller)
        {
            _sellersList.Remove(seller);
        }

        //**@brief Devuelve la lista completa de vendedores.
        public List<Seller> ReturnSellers()
        {
            return _sellersList;
        }

        /**
         * @brief Verifica si existe un vendedor con un valor específico en cualquier atributo.
         *
         * @param attribute Nombre del atributo (por ejemplo: "Email", "FirstName").
         * @param value Valor a buscar.
         * @return true si existe al menos un vendedor con ese valor; false en caso contrario.
         */
        public bool ExistsSeller(string attribute, object value)
        {
            foreach (var seller in _sellersList)
            {
                PropertyInfo? prop = seller.GetType().GetProperty(attribute);
                if (prop != null)
                {
                    var propValue = prop.GetValue(seller);
                    if (propValue != null && propValue.Equals(value))
                        return true;
                }
            }
            return false;
        }

        /**
        * @brief Devuelve un vendedor que coincida con nombre y apellido.
        *
        * @param firstName Nombre del vendedor.
        * @param lastName Apellido del vendedor.
        * @return Instancia Seller si se encuentra; null si no existe.
        */
        public Seller? ReturnSeller(string firstName, string lastName)
        {
            return _sellersList.FirstOrDefault(s =>
                s.FirstName == firstName && s.LastName == lastName);
        }

    }
}
