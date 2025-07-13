using backend_expendedora.Models;
using backend_expendedora.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace PlanillaTest.UnitTests.InventarioServiceTests
{
    public class InventarioServiceTests
    {
        [SetUp]
        public void SetUp()
        {
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
        public void RefrescosInicialesDeberianContenerCocaColaTest()
        {
            var tieneCoca = InventarioService.Refrescos.ContainsKey("Coca-Cola");

            Assert.That(tieneCoca, Is.True);
        }

        [Test]
        public void DineroInicialDeberiaTenerCantidadEsperadaTest()
        {
            var cantidad500 = InventarioService.Dinero[500];

            Assert.That(cantidad500, Is.EqualTo(20));
        }

        [Test]
        public void ActualizarStockDeberiaReducirCorrectamenteTest()
        {
            var antes = InventarioService.Refrescos["Pepsi"].Stock;

            InventarioService.Refrescos["Pepsi"].Stock -= 2;

            var despues = InventarioService.Refrescos["Pepsi"].Stock;

            Assert.That(despues, Is.EqualTo(antes - 2));
        }

        [Test]
        public void AgregarDineroDeberiaAumentarCantidadTest()
        {
            var antes = InventarioService.Dinero[100];

            InventarioService.Dinero[100] += 5;

            var despues = InventarioService.Dinero[100];

            Assert.That(despues, Is.EqualTo(antes + 5));
        }
    }
}

