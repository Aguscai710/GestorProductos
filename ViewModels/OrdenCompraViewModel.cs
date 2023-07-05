using PNT1_Grupo6.Models;

namespace PNT1_Grupo6.ViewModels
{
    public class OrdenCompraViewModel
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }
        public EstadoOrdenCompra Estado { get; set; }
    }
}

