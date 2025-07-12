namespace backend_expendedora.Models
{
    public class PagoException : Exception
    {
        public PagoException(string mensaje) : base(mensaje) { }
    }
}
