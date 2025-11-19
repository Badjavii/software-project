using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;

namespace backend.controllers
{
    /**
     * @class SellerController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con vendedores.
     *
     * Expone endpoints para registrar y consultar perfiles de tipo Seller.
     */
    [ApiController]
    [Route("api/seller")]
    public class SellerController : ControllerBase
    {
        private readonly SellerService _service = new SellerService();

        /**
         * @brief Endpoint para registrar un nuevo vendedor.
         * @param dto Datos del vendedor enviados desde el frontend.
         * @return Resultado HTTP con el vendedor creado o error.
         */
        [HttpPost("register")]
        public IActionResult RegisterSeller([FromBody] SellerDTO dto)
        {
            try
            {
                Seller seller = _service.RegisterSeller(dto);
                return Ok(seller);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Endpoint para consultar un vendedor por nombre y apellido.
         * @param firstName Nombre del vendedor.
         * @param lastName Apellido del vendedor.
         * @return Resultado HTTP con el vendedor encontrado o error.
         */
        [HttpGet("get")]
        public IActionResult GetSeller([FromQuery] string firstName, [FromQuery] string lastName)
        {
            Seller? seller = _service.GetSeller(firstName, lastName);
            if (seller == null)
                return NotFound(new { error = "Vendedor no encontrado." });

            return Ok(seller);
        }
    }
}
