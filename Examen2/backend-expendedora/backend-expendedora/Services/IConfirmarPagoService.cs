using backend_expendedora.Models;

namespace backend_expendedora.Services
{
    public interface IConfirmarPagoService
    {
        object Ejecutar(PagoRequest solicitud);
    }
}
