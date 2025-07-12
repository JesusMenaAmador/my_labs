using Microsoft.AspNetCore.Mvc;
using backend_expendedora.Models;
using backend_expendedora.Services;
using backend_expendedora.Application.Handlers;

namespace backend_expendedora.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class InventarioController : ControllerBase
    {
        private readonly ConfirmarPagoHandler _confirmarPagoHandler;

        public InventarioController(ConfirmarPagoHandler confirmarPagoHandler)
        {
            _confirmarPagoHandler = confirmarPagoHandler;
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
        public IActionResult ConfirmarPago([FromBody] PagoRequest solicitud)
        {
            return _confirmarPagoHandler.Ejecutar(solicitud);
        }
    }
}

