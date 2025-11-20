using backend.models;
using backend.repositories;

namespace backend.services
{
    /**
     * @class UserService
     * @brief Servicio que encapsula la lógica de negocio relacionada con usuarios.
     *
     * Aplica reglas de negocio: solo correos UCAB, no duplicar correos,
     * y manejo especial al registrar vendedores.
     */
    public class UserService
    {
        private readonly UserRepository _repository = UserRepository.Instance;

        /**
         * @brief Registra un nuevo comprador en el sistema.
         * @param dto Objeto DTO con los datos del comprador.
         * @return Instancia User creada.
         * @exception Exception Si el correo no es UCAB o ya está registrado.
         */
        public User RegisterBuyer(UserDTO dto)
        {
            ValidateUcabEmail(dto.Email);

            if (_repository.ReturnUserByEmail(dto.Email) != null)
                throw new Exception("Ya existe un usuario con ese correo.");

            User user = new User(dto.Email, dto.FirstName, dto.LastName, dto.Age, dto.Password);
            _repository.AddBuyer(user);
            _repository.Save();
            return user;
        }

        /**
         * @brief Registra un nuevo vendedor en el sistema.
         * @param dto Objeto DTO con los datos del vendedor.
         * @param info Información adicional del vendedor (SellerInfo).
         * @return Instancia User creada o actualizada.
         * @exception Exception Si el correo no es UCAB o ya está registrado como vendedor.
         */
        public User RegisterSeller(UserDTO dto, SellerInfo info)
        {
            ValidateUcabEmail(dto.Email);

            var existingUser = _repository.ReturnUserByEmail(dto.Email);

            if (existingUser != null)
            {
                if (existingUser.IsSeller)
                    throw new Exception("Ya existe un vendedor con ese correo.");

                // Si existe como comprador, cambiar rol a vendedor
                existingUser.ActivateSellerRole(info);
                _repository.Save();
                return existingUser;
            }

            // Si no existe, crear nuevo usuario vendedor
            User user = new User(dto.Email, dto.FirstName, dto.LastName, dto.Age, dto.Password);
            user.ActivateSellerRole(info);
            _repository.AddSeller(user, info);
            _repository.Save();
            return user;
        }

        /**
         * @brief Consulta un usuario por correo.
         * @param email Correo del usuario.
         * @return Instancia User si se encuentra; null si no existe.
         */
        public User? GetUserByEmail(string email)
        {
            return _repository.ReturnUserByEmail(email);
        }

        /**
         * @brief Valida que el correo pertenezca al dominio UCAB.
         * @param email Correo a validar.
         * @exception Exception Si el correo no es UCAB.
         */
        private void ValidateUcabEmail(string email)
        {
            if (!email.EndsWith("ucab.edu.ve", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Solo se permiten correos UCAB.");
        }
    }

    /**
     * @class UserDTO
     * @brief Objeto de transferencia de datos para registrar usuarios.
     */
    public class UserDTO
    {
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Password { get; set; }
    }
}
