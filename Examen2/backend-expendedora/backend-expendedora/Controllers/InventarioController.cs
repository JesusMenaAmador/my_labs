using Microsoft.AspNetCore.Mvc;
using backend_expendedora.Models;
using backend_expendedora.Services;

namespace backend_expendedora.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class InventarioController : ControllerBase
    {
        private readonly IConfirmarPagoService _confirmarPagoService;

        public InventarioController(IConfirmarPagoService confirmarPagoService)
        {
            _confirmarPagoService = confirmarPagoService;
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
            var resultado = _confirmarPagoService.Ejecutar(solicitud);

            if (resultado?.GetType().GetProperty("error")?.GetValue(resultado)?.Equals(true) == true)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}

