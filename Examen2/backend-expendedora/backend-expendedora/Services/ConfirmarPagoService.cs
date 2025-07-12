using backend_expendedora.Models;

namespace backend_expendedora.Services
{
    public class ConfirmarPagoService : IConfirmarPagoService
    {
        private readonly IPagoService _pagoService;

        public ConfirmarPagoService(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        public object Ejecutar(PagoRequest solicitud)
        {
            try
            {
                var resultado = _pagoService.ProcesarPago(solicitud);
                return resultado;
            }
            catch (PagoException ex)
            {
                return new { error = true, mensaje = ex.Message };
            }
        }

    }
}

