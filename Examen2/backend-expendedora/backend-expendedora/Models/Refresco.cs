namespace backend_expendedora.Models
{
    public class Refresco
    {
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }

        public Refresco(string nombre, int precio, int stock)
        {
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
        }
    }
}
