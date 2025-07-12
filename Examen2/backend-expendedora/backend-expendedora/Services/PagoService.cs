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

            // Caso: dinero insuficiente
            if (ingresado < total)
            {
                return new PagoResponse
                {
                    Mensaje = "Dinero insuficiente. Se devuelve el monto ingresado.",
                    Vuelto = new Dictionary<int, int>(solicitud.DineroIngresado)
                };
            }

            int cambio = ingresado - total;

            // Calcular el cambio
            var vuelto = CalcularCambio(cambio);

            if (vuelto == null)
            {
                if (cambio > 0)
                    throw new PagoException("Fuera de servicio por falta de cambio");

                // Pago exacto, no hay error
                return new PagoResponse
                {
                    Mensaje = "Pago exacto",
                    Vuelto = new Dictionary<int, int>()
                };
            }

            // Actualizar inventario de refrescos
            foreach (var item in solicitud.RefrescosSeleccionados)
            {
                InventarioService.Refrescos[item.Key].Stock -= item.Value;
            }

            // Sumar dinero ingresado
            foreach (var kvp in solicitud.DineroIngresado)
            {
                InventarioService.Dinero[kvp.Key] += kvp.Value;
            }

            // Restar dinero usado como vuelto
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

        // Método que calcula el cambio con monedas
        private Dictionary<int, int>? CalcularCambio(int cambio)
        {
            var disponible = InventarioService.Dinero;

            var denominaciones = disponible.Keys
                .Where(x => x <= 500) // Solo monedas
                .OrderByDescending(x => x)
                .ToList();

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
