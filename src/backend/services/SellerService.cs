using backend.models;
using backend.factories;
using backend.repositories;
using ProfileFactory = backend.factories.ProfileFactory;

namespace backend.services
{
    /**
     * @class SellerService
     * @brief Servicio que encapsula la lógica de negocio relacionada con vendedores.
     *
     * Permite registrar nuevos vendedores, consultar perfiles y verificar existencia.
     * Si el correo ya está registrado como comprador, elimina al comprador antes de registrar al vendedor.
     */
    public class SellerService
    {
        private readonly SellerRepository _sellerRepository = SellerRepository.Instance;
        private readonly BuyerRepository _buyerRepository = BuyerRepository.Instance;

        /**
         * @brief Registra un nuevo vendedor en el sistema.
         * @param dto Objeto DTO con los datos del vendedor.
         * @return Instancia Seller creada.
         * @exception Exception Si el correo ya está registrado como vendedor.
         */
        public Seller RegisterSeller(SellerDTO dto)
        {
            if (_sellerRepository.ExistsSeller("_email", dto.Email))
                throw new Exception("Ya existe un vendedor con ese correo.");

            if (_sellerRepository.ReturnSeller(dto.FirstName, dto.LastName) != null)
                throw new Exception("Ya existe un vendedor con ese nombre y apellido.");

            // Si el correo ya existe como comprador, eliminarlo
            if (_buyerRepository.ExistsBuyer("_email", dto.Email))
            {
                Buyer? comprador = _buyerRepository.ReturnBuyer(dto.FirstName, dto.LastName);
                if (comprador != null)
                {
                    _buyerRepository.RemoveBuyer(comprador);
                    _buyerRepository.Save();
                }
            }

            Seller seller = ProfileFactory.CreateSeller(dto.Email, dto.FirstName, dto.LastName, dto.Age, dto.Password);
            _sellerRepository.AddSeller(seller);
            _sellerRepository.Save();
            return seller;
        }

        /**
         * @brief Consulta un vendedor por nombre y apellido.
         * @param firstName Nombre del vendedor.
         * @param lastName Apellido del vendedor.
         * @return Instancia Seller si se encuentra; null si no existe.
         */
        public Seller? GetSeller(string firstName, string lastName)
        {
            return _sellerRepository.ReturnSeller(firstName, lastName);
        }
    }

    /**
     * @class SellerDTO
     * @brief Objeto de transferencia de datos para registrar vendedores.
     */
    public class SellerDTO
    {
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Password { get; set; }
    }
}
