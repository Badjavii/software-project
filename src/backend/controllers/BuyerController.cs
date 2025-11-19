using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;

namespace backend.controllers
{
    /**
     * @class BuyerController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con compradores.
     *
     * Expone endpoints para registrar y consultar perfiles de tipo Buyer.
     */
    [ApiController]
    [Route("api/buyer")]
    public class BuyerController : ControllerBase
    {
        private readonly BuyerService _service = new BuyerService();

        /**
         * @brief Endpoint para registrar un nuevo comprador.
         * @param dto Datos del comprador enviados desde el frontend.
         * @return Resultado HTTP con el comprador creado o error.
         */
        [HttpPost("register")]
        public IActionResult RegisterBuyer([FromBody] BuyerDTO dto)
        {
            try
            {
                Buyer buyer = _service.RegisterBuyer(dto);
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Endpoint para consultar un comprador por nombre y apellido.
         * @param firstName Nombre del comprador.
         * @param lastName Apellido del comprador.
         * @return Resultado HTTP con el comprador encontrado o error.
         */
        [HttpGet("get")]
        public IActionResult GetBuyer([FromQuery] string firstName, [FromQuery] string lastName)
        {
            Buyer? buyer = _service.GetBuyer(firstName, lastName);
            if (buyer == null)
                return NotFound(new { error = "Comprador no encontrado." });

            return Ok(buyer);
        }
    }
}
