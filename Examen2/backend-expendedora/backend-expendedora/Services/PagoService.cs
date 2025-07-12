using backend_expendedora.Models;

namespace backend_expendedora.Services
{
    public class PagoService : IPagoService
    {
        public PagoResponse ProcesarPago(PagoRequest solicitud)
        {
            int total = 0;

            foreach (var item in solicitud.RefrescosSeleccionados)
            {
                if (!InventarioService.Refrescos.ContainsKey(item.Key))
                    throw new PagoException("Refresco inválido");

                var refresco = InventarioService.Refrescos[item.Key];

                if (item.Value > refresco.Stock)
                    throw new PagoException($"No hay suficientes unidades de {refresco.Nombre}");

                total += item.Value * refresco.Precio;
            }

            int ingresado = 0;
            foreach (var kvp in solicitud.DineroIngresado)
            {
                if (!InventarioService.Dinero.ContainsKey(kvp.Key))
                    throw new PagoException("Denominación no aceptada");

                if (kvp.Value < 0 || kvp.Value > 999)
                    throw new PagoException("Cantidad inválida de una denominación");

                ingresado += kvp.Key * kvp.Value;
            }

            if (ingresado < total)
                throw new PagoException("Dinero insuficiente");

            int cambio = ingresado - total;

            var vuelto = CalcularCambio(cambio);

            if (vuelto == null)
                throw new PagoException("Fallo al realizar la compra");

            if (vuelto.Values.All(c => c == 0) && cambio > 0)
                throw new PagoException("Fuera de servicio");

            foreach (var item in solicitud.RefrescosSeleccionados)
            {
                InventarioService.Refrescos[item.Key].Stock -= item.Value;
            }

            foreach (var kvp in solicitud.DineroIngresado)
            {
                InventarioService.Dinero[kvp.Key] += kvp.Value;
            }

            foreach (var kvp in vuelto)
            {
                InventarioService.Dinero[kvp.Key] -= kvp.Value;
            }

            return new PagoResponse
            {
                Mensaje = "Pago exitoso",
                Vuelto = vuelto
            };
        }

        private Dictionary<int, int>? CalcularCambio(int cambio)
        {
            var disponible = InventarioService.Dinero;
            var denominaciones = disponible.Keys.OrderByDescending(x => x).ToList();
            var resultado = new Dictionary<int, int>();

            foreach (var d in denominaciones)
            {
                int cantidadDisponible = disponible[d];
                int cantidadNecesaria = cambio / d;
                int cantidadUsada = Math.Min(cantidadDisponible, cantidadNecesaria);

                if (cantidadUsada > 0)
                {
                    resultado[d] = cantidadUsada;
                    cambio -= cantidadUsada * d;
                }
            }

            if (cambio == 0)
            {
                foreach (var d in denominaciones)
                {
                    if (!resultado.ContainsKey(d))
                        resultado[d] = 0;
                }

                return resultado;
            }

            return null;
        }
    }
}


