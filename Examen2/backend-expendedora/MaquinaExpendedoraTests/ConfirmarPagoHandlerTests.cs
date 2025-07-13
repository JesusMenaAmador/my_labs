using backend_expendedora.Application.Handlers;
using backend_expendedora.Models;
using backend_expendedora.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace PlanillaTest.UnitTests.ConfirmarPagoHandlerTests
{
    public class ConfirmarPagoHandlerTests
    {
        private Mock<IConfirmarPagoService> _mockService = null!;
        private ConfirmarPagoHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IConfirmarPagoService>();
            _handler = new ConfirmarPagoHandler(_mockService.Object);
        }

        [Test]
        public void EjecutarCuandoHayErrorDeberiaRetornarBadRequestTest()
        {
            var solicitud = new PagoRequest();

            var resultadoConError = new { error = true, mensaje = "Fondos insuficientes" };

            _mockService.Setup(s => s.Ejecutar(solicitud)).Returns(resultadoConError);

            var response = _handler.Ejecutar(solicitud);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void EjecutarCuandoNoHayErrorDeberiaRetornarOkTest()
        {
            var solicitud = new PagoRequest();

            var resultadoExitoso = new { mensaje = "Pago confirmado" };

            _mockService.Setup(s => s.Ejecutar(solicitud)).Returns(resultadoExitoso);

            var response = _handler.Ejecutar(solicitud);

            Assert.That(response, Is.InstanceOf<OkObjectResult>());
        }
    }
}
