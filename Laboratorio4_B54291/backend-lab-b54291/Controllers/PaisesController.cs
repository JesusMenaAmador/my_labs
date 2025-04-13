using backend_lab_b54291.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_lab_b54291.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly PaisHandler _paisesHandler;
        
        public PaisesController() 
        {
            _paisesHandler = new PaisHandler();
        }

        [HttpGet]
        public string Get()
        {
            var paises = _paisesHandler.ObtenerPaises();
            return "Hola Mundo";
        }
    }
}
