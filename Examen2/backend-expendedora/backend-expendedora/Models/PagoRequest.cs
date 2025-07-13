namespace backend_expendedora.Models
{
    public class PagoRequest
    {
        public Dictionary<string, int> RefrescosSeleccionados { get; set; } = new();
        public Dictionary<int, int> DineroIngresado { get; set; } = new();
    }
}
