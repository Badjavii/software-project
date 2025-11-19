namespace backend.models
{
    /**
     * @class ProfileFactory
     * @brief Fábrica para crear instancias de perfiles de usuario (Buyer o Seller).
     *
     * Implementa el patrón Singleton para garantizar una única instancia de la fábrica.
     * Proporciona métodos estáticos para construir perfiles según el tipo de usuario.
     */
    public class ProfileFactory
    {
        //**@brief Instancia única de la fábrica (Singleton).
        private static readonly ProfileFactory _instance = new ProfileFactory();

        //**@brief Constructor privado para evitar instanciación externa.
        private ProfileFactory() {}

        //**@brief Obtiene la instancia única de la fábrica.
        public static ProfileFactory Instance => _instance;

        //**@brief Crea un perfil de tipo Buyer.
        public static Buyer CreateBuyer(string email, string firstName, string lastName, int age, string password)
        {
            return new Buyer(email, firstName, lastName, age, password);
        }

        //**@brief Crea un perfil de tipo Seller. Retorna instancia de Buyer.
        public static Seller CreateSeller(string email, string firstName, string lastName, int age, string password, List<Book> catalog, List<float> ratings)
        {
            Seller seller = new Seller(email, firstName, lastName, age, password);
            seller.Catalog = catalog ?? new List<Book>();
            seller.Ratings = ratings ?? new List<float>();
            return seller;
        }
    }
}
