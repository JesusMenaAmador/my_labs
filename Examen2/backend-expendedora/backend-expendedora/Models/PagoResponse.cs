namespace backend_expendedora.Models
{
    public class PagoResponse
    {
        public string Mensaje { get; set; } = "";
        public Dictionary<int, int> Vuelto { get; set; } = new();
    }
}