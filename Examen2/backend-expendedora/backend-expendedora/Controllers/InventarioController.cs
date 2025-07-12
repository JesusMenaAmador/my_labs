using Microsoft.AspNetCore.Mvc;
using backend_expendedora.Models;
using backend_expendedora.Services;

namespace backend_expendedora.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class InventarioController : ControllerBase
    {
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
    }
}
