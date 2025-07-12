using backend_expendedora.Models;

namespace backend_expendedora.Services
{
    public static class InventarioService
    {
        public static Dictionary<string, Refresco> Refrescos = new()
        {
            { "Coca-Cola", new Refresco("Coca-Cola", 800, 10) },
            { "Pepsi", new Refresco("Pepsi", 750, 8) },
            { "Fanta", new Refresco("Fanta", 950, 10) },
            { "Sprite", new Refresco("Sprite", 975, 15) }
        };

        public static Dictionary<int, int> Dinero = new()
        {
            { 1000, 0 },
            { 500, 20 },
            { 100, 30 },
            { 50, 50 },
            { 25, 25 }
        };
    }
}
