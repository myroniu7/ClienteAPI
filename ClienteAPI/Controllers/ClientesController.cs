using Microsoft.AspNetCore.Mvc;
using ClienteAPI.Services;
using ClienteAPI.DTOs;

namespace ClienteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtiene los datos de un cliente por su número de identificación
        /// </summary>
        /// <param name="identificacion">Número de identificación del cliente</param>
        /// <returns>Datos del cliente si existe</returns>
        [HttpGet("{identificacion}")]
        [ProducesResponseType(typeof(ClienteResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ClienteResponseDto>> GetCliente(int identificacion)
        {
            var resultado = await _clienteService.ObtenerClientePorIdentificacionAsync(identificacion);

            if (!resultado.Existe)
            {
                return NotFound(resultado);
            }

            return Ok(resultado);
        }
    }
}