using System.Text.Json;
using System.Text.Json.Serialization;
using backend.models;
using backend.factories;
using System.Reflection;
using ProfileFactory = backend.factories.ProfileFactory;

namespace backend.repositories
{
    /**
     * @class BuyerRepository
     * @brief Repositorio que gestiona la colección de compradores en el sistema Booksy.
     *
     * Implementa el patrón Singleton. Carga los datos desde un archivo JSON al iniciar
     * y permite agregar, eliminar, buscar y exportar compradores.
     */
    public class BuyerRepository
    {
        //**@brief Instancia única del repositorio (Singleton).
        private static readonly BuyerRepository _instance = new BuyerRepository();

        //**@brief Ruta del archivo JSON que contiene los datos de los compradores.
        private readonly string _jsonPath = @"data/buyers.json";

        //**@brief Lista interna de compradores cargados desde el archivo.
        private List<Buyer> _buyers;

        //**@brief Constructor privado. Llama al método Load() para inicializar la lista.
        private BuyerRepository()
        {
            _buyers = new List<Buyer>();
            Load();
        }

        //**@brief Obtiene la instancia única del repositorio.
        public static BuyerRepository Instance => _instance;

        /**
         * @brief Carga los datos de compradores desde el archivo JSON.
         *
         * Lee cada objeto, extrae sus atributos, crea instancias con ProfileFactory y las agrega a la lista.
         */
        private void Load()
        {
            if (!File.Exists(_jsonPath)) return;

            string json = File.ReadAllText(_jsonPath);
            var rawBuyers = JsonSerializer.Deserialize<List<JsonElement>>(json);

            if (rawBuyers == null) return;

            foreach (var element in rawBuyers)
            {
                string email = element.GetProperty("_email").GetString()!;
                string firstName = element.GetProperty("_firstName").GetString()!;
                string lastName = element.GetProperty("_lastName").GetString()!;
                int age = element.GetProperty("_age").GetInt32()!;
                string password = element.GetProperty("_password").GetString()!;

                Buyer buyer = ProfileFactory.CreateBuyer(email, firstName, lastName, age, password);
                _buyers.Add(buyer);
            }
        }

        //**@brief Guarda todos los compradores en el archivo JSON, sobrescribiendo el contenido.
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_buyers, options);
            File.WriteAllText(_jsonPath, json);
        }

        //**@brief Agrega un nuevo comprador al repositorio.
        public void AddBuyer(Buyer buyer)
        {
            _buyers.Add(buyer);
        }

        //**@brief Elimina un comprador del repositorio.
        public void RemoveBuyer(Buyer buyer)
        {
            _buyers.Remove(buyer);
        }

        //**@brief Busca y retorna un comprador específico por nombre y apellido.
        public Buyer? ReturnBuyer(string firstName, string lastName)
        {
            return _buyers.FirstOrDefault(b =>
                b.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                b.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        /**
         * @brief Verifica si existe un comprador con un valor específico en cualquier atributo.
         *
         * @param attribute Nombre del atributo (por ejemplo: "Email", "FirstName").
         * @param value Valor a buscar.
         * @return true si existe al menos un comprador con ese valor; false en caso contrario.
         */
        public bool ExistsBuyer(string attribute, object value)
        {
            foreach (var buyer in _buyers)
            {
                PropertyInfo? prop = buyer.GetType().GetProperty(attribute);
                if (prop != null)
                {
                    var propValue = prop.GetValue(buyer);
                    if (propValue != null && propValue.Equals(value))
                        return true;
                }
            }
            return false;
        }
    }
}
