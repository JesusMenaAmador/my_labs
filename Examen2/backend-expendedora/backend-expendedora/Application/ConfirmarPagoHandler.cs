using backend_expendedora.Models;
using backend_expendedora.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_expendedora.Application.Handlers
{
    public class ConfirmarPagoHandler
    {
        private readonly IConfirmarPagoService _confirmarPagoService;

        public ConfirmarPagoHandler(IConfirmarPagoService confirmarPagoService)
        {
            _confirmarPagoService = confirmarPagoService;
        }

        public IActionResult Ejecutar(PagoRequest solicitud)
        {
            var resultado = _confirmarPagoService.Ejecutar(solicitud);

            bool esError =
                resultado?.GetType().GetProperty("error")?.GetValue(resultado)?.Equals(true) == true;

            if (esError)
                return new BadRequestObjectResult(resultado);

            return new OkObjectResult(resultado);
        }
    }
}
