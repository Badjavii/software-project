using backend.models;
using backend.factories;
using backend.repositories;
using ProfileFactory = backend.factories.ProfileFactory;

namespace backend.services
{
    /**
     * @class BuyerService
     * @brief Servicio que encapsula la lógica de negocio relacionada con compradores.
     *
     * Permite registrar nuevos compradores, consultar perfiles y verificar existencia.
     */
    public class BuyerService
    {
        private readonly BuyerRepository _repository = BuyerRepository.Instance;

        /**
         * @brief Registra un nuevo comprador en el sistema.
         * @param dto Objeto DTO con los datos del comprador.
         * @return Instancia Buyer creada.
         * @exception Exception Si el correo ya está registrado.
         */
        public Buyer RegisterBuyer(BuyerDTO dto)
        {
            if (_repository.ExistsBuyer("_email", dto.Email) == true)
                throw new Exception("Ya existe un comprador con ese correo.");

            if (_repository.ReturnBuyer(dto.FirstName, dto.LastName) != null)
                throw new Exception("Ya existe un comprador con ese nombre y apellido.");

            Buyer buyer = ProfileFactory.CreateBuyer(dto.Email, dto.FirstName, dto.LastName, dto.Age, dto.Password);
            _repository.AddBuyer(buyer);
            _repository.Save();
            return buyer;
        }

        /**
         * @brief Consulta un comprador por nombre y apellido.
         * @param firstName Nombre del comprador.
         * @param lastName Apellido del comprador.
         * @return Instancia Buyer si se encuentra; null si no existe.
         */
        public Buyer? GetBuyer(string firstName, string lastName)
        {
            return _repository.ReturnBuyer(firstName, lastName);
        }
    }

    /**
     * @class BuyerDTO
     * @brief Objeto de transferencia de datos para registrar compradores.
     */
    public class BuyerDTO
    {
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Password { get; set; }
    }

}
