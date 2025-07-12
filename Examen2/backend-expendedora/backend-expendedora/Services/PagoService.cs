using backend_expendedora.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend_expendedora.Services
{
    public class PagoService : IPagoService
    {
        public PagoResponse ProcesarPago(PagoRequest solicitud)
        {
            int total = 0;

            // Verificar los refrescos seleccionados y calcular el total
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
            // Verificar el dinero ingresado
            foreach (var kvp in solicitud.DineroIngresado)
            {
                if (!InventarioService.Dinero.ContainsKey(kvp.Key))
                    throw new PagoException("Denominación no aceptada");

                if (kvp.Value < 0 || kvp.Value > 999)
                    throw new PagoException("Cantidad inválida de una denominación");

                ingresado += kvp.Key * kvp.Value;
            }

            // Verificar si el dinero ingresado es suficiente
            if (ingresado < total)
                throw new PagoException("Dinero insuficiente");

            int cambio = ingresado - total;

            // Calcular el cambio
            var vuelto = CalcularCambio(cambio);

            if (vuelto == null)
                throw new PagoException("Fallo al realizar la compra");

            // Verificar si no hay suficiente vuelto
            if (vuelto.Values.All(c => c == 0) && cambio > 0)
                throw new PagoException("Fuera de servicio");

            // Actualizar el inventario de refrescos y dinero
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

        // Método que calcula el cambio solo con monedas (500, 100, 50, 25)
        private Dictionary<int, int>? CalcularCambio(int cambio)
        {
            var disponible = InventarioService.Dinero;

            // Filtrar solo las monedas (500, 100, 50, 25)
            var denominaciones = disponible.Keys
                .Where(x => x <= 500) // Incluir solo monedas
                .OrderByDescending(x => x) // Ordenar de mayor a menor
                .ToList();

            var resultado = new Dictionary<int, int>();

            // Recorremos las denominaciones y calculamos el vuelto
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

            // Si no hay cambio restante, devolvemos el resultado
            if (cambio == 0)
            {
                // Aseguramos que todas las denominaciones se muestren, aunque sea con valor 0
                foreach (var d in denominaciones)
                {
                    if (!resultado.ContainsKey(d))
                        resultado[d] = 0; // Asignar 0 si no se ha usado esa denominación
                }
                return resultado;
            }

            // Si no se puede dar el cambio completo, devolvemos null
            return null;
        }
    }
}
