using backend_expendedora.Models;
using backend_expendedora.Services;
using Moq;

namespace PlanillaTest.UnitTests.ConfirmarPagoServiceTests
{
    public class ConfirmarPagoServiceTests
    {
        private Mock<IPagoService> _pagoServiceMock = null!;
        private ConfirmarPagoService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _pagoServiceMock = new Mock<IPagoService>();
            _service = new ConfirmarPagoService(_pagoServiceMock.Object);
        }

        [Test]
        public void EjecutarCuandoPagoEsValidoDeberiaRetornarPagoResponseTest()
        {
            var request = new PagoRequest();
            var expectedResponse = new PagoResponse
            {
                Mensaje = "Pago exitoso",
                Vuelto = new Dictionary<int, int> { { 100, 1 } }
            };

            _pagoServiceMock.Setup(p => p.ProcesarPago(request)).Returns(expectedResponse);

            var resultado = _service.Ejecutar(request);

            Assert.That(resultado, Is.EqualTo(expectedResponse));
        }

        [Test]
        public void EjecutarCuandoHayExcepcionDeberiaRetornarErrorTest()
        {
            var request = new PagoRequest();
            var mensajeError = "Error de prueba";

            _pagoServiceMock
                .Setup(p => p.ProcesarPago(request))
                .Throws(new PagoException(mensajeError));

            var resultado = _service.Ejecutar(request);

            var tipo = resultado.GetType();
            var error = (bool?)tipo.GetProperty("error")?.GetValue(resultado);
            var mensaje = (string?)tipo.GetProperty("mensaje")?.GetValue(resultado);

            Assert.That(error, Is.True);
            Assert.That(mensaje, Is.EqualTo(mensajeError));
        }
    }
}

