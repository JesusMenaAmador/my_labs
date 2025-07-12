using Microsoft.AspNetCore.Mvc;
using backend_expendedora.Models;
using backend_expendedora.Services;

namespace backend_expendedora.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class InventarioController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public InventarioController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet("refrescos")]
        public ActionResult<IEnumerable<Refresco>> ObtenerRefrescos()
        {
            return Ok(InventarioService.Refrescos.Values);
        }

        [HttpGet("dinero")]
        public ActionResult<Dictionary<int, int>> ObtenerDinero()
        {
            return Ok(InventarioService.Dinero);
        }

        [HttpPost("confirmar-pago")]
        public ActionResult ConfirmarPago([FromBody] PagoRequest solicitud)
        {
            try
            {
                _pagoService.ProcesarPago(solicitud);
                return Ok(new { mensaje = "Compra realizada con éxito" });
            }
            catch (PagoException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

