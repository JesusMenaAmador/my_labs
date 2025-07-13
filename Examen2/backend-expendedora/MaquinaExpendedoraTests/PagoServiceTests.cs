using backend_expendedora.Models;
using backend_expendedora.Services;

namespace PlanillaTest.UnitTests.PagoServiceTests
{
    public class PagoServiceTests
    {
        private PagoService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _service = new PagoService();

            InventarioService.Refrescos = new Dictionary<string, Refresco>
            {
                { "Coca-Cola", new Refresco("Coca-Cola", 800, 10) },
                { "Pepsi", new Refresco("Pepsi", 750, 8) },
                { "Fanta", new Refresco("Fanta", 950, 10) },
                { "Sprite", new Refresco("Sprite", 975, 15) }
            };

            InventarioService.Dinero = new Dictionary<int, int>
            {
                { 1000, 0 },
                { 500, 20 },
                { 100, 30 },
                { 50, 50 },
                { 25, 25 }
            };
        }

        [Test]
        public void ProcesarPagoCuandoRefrescoNoExisteDeberiaLanzarExcepcionTest()
        {
            var request = new PagoRequest
            {
                RefrescosSeleccionados = new() { { "Inexistente", 1 } },
                DineroIngresado = new() { { 1000, 1 } }
            };

            Assert.Throws<PagoException>(() => _service.ProcesarPago(request));
        }

        [Test]
        public void ProcesarPagoCuandoStockInsuficienteDeberiaLanzarExcepcionTest()
        {
            var request = new PagoRequest
            {
                RefrescosSeleccionados = new() { { "Pepsi", 99 } },
                DineroIngresado = new() { { 1000, 1 } }
            };

            Assert.Throws<PagoException>(() => _service.ProcesarPago(request));
        }

        [Test]
        public void ProcesarPagoCuandoDineroInsuficienteDeberiaRetornarMensajeErrorTest()
        {
            var request = new PagoRequest
            {
                RefrescosSeleccionados = new() { { "Coca-Cola", 1 } },
                DineroIngresado = new() { { 500, 1 } }
            };

            var result = _service.ProcesarPago(request);

            Assert.That(result.Mensaje, Does.Contain("Dinero insuficiente"));
            Assert.That(result.Vuelto[500], Is.EqualTo(1));
        }

        [Test]
        public void ProcesarPagoConCambioDeberiaRetornarVueltoCorrectoTest()
        {
            var request = new PagoRequest
            {
                RefrescosSeleccionados = new() { { "Pepsi", 1 } },
                DineroIngresado = new() { { 1000, 1 } }
            };

            var result = _service.ProcesarPago(request);

            Assert.That(result.Mensaje, Does.Contain("Pago exitoso"));
            Assert.That(result.Vuelto.Values.Sum(), Is.GreaterThan(0));
        }

        [Test]
        public void ProcesarPagoSinCambioDisponibleDeberiaRetornarFueraDeServicioTest()
        {
            InventarioService.Dinero = new Dictionary<int, int>
            {
                { 1000, 0 },
                { 500, 0 },
                { 100, 0 },
                { 50, 0 },
                { 25, 0 }
            };

            var request = new PagoRequest
            {
                RefrescosSeleccionados = new() { { "Pepsi", 1 } },
                DineroIngresado = new() { { 1000, 1 } }
            };

            var result = _service.ProcesarPago(request);

            Assert.That(result.Mensaje, Does.Contain("Fuera de servicio"));
            Assert.That(result.Vuelto[1000], Is.EqualTo(1));
        }
    }
}

