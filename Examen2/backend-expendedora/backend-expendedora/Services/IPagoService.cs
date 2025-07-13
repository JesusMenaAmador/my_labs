using backend_expendedora.Models;

namespace backend_expendedora.Services
{
    public interface IPagoService
    {
        PagoResponse ProcesarPago(PagoRequest solicitud);
    }
}
