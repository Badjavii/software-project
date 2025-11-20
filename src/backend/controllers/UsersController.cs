using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;
using backend.repositories;

namespace backend.controllers
{
    /**
     * @class UserController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con usuarios.
     *
     * Expone endpoints para registrar compradores, vendedores, consultar perfiles y login.
     */
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service = new UserService();
        private readonly BookRepository _bookRepository = BookRepository.Instance;

        /**
         * @brief Endpoint para registrar un nuevo comprador.
         */
        [HttpPost("register-buyer")]
        public IActionResult RegisterBuyer([FromBody] UserDTO dto)
        {
            try
            {
                User user = _service.RegisterBuyer(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Endpoint para registrar un nuevo vendedor.
         */
        [HttpPost("register-seller")]
        public IActionResult RegisterSeller([FromBody] UserDTO dto, [FromQuery] string bankName, [FromQuery] string accountNumber, [FromQuery] string phoneNumber)
        {
            try
            {
                var catalog = _bookRepository.GetCatalogBySellerEmail(dto.Email);
                var sellerInfo = new SellerInfo(bankName, accountNumber, phoneNumber, catalog, new List<float>());
                User user = _service.RegisterSeller(dto, sellerInfo);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Endpoint para consultar un usuario por correo.
         */
        [HttpGet("get")]
        public IActionResult GetUser([FromQuery] string email)
        {
            User? user = _service.GetUserByEmail(email);
            if (user == null)
                return NotFound(new { error = "Usuario no encontrado." });

            return Ok(user);
        }

        /**
         * @brief Endpoint para obtener el historial de compras de un usuario.
         */
        [HttpGet("purchase-history")]
        public IActionResult GetPurchaseHistory([FromQuery] string email)
        {
            var books = _bookRepository.GetPurchaseHistoryByBuyerEmail(email);
            if (books.Count == 0)
                return NotFound(new { error = "No se encontraron compras para este usuario." });

            return Ok(books);
        }

        /**
         * @brief Endpoint para login de usuario con validación de contraseña.
         */
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string email, [FromQuery] string password)
        {
            User? user = _service.GetUserByEmail(email);

            if (user == null)
                return NotFound(new { error = "Usuario no encontrado." });

            if (!user.VerifyPassword(password))
                return Unauthorized(new { error = "Acceso denegado: contraseña incorrecta." });

            return Ok(new
            {
                message = "Login exitoso.",
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                isSeller = user.IsSeller,
                purchaseHistory = user.PurchaseHistory
            });
        }
    }
}
