using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PNT1_Grupo6.Models
{
	public class OrdenCompra
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Display(Name = "Código del proveedor")]
		[Required(ErrorMessage = "Indique el proveedor para la compra.")]
		public int ProveedorId { get; set; }

		[Display(Name = "Código del producto")]
		[Required(ErrorMessage = "Indique el prodcuto a comprar.")]
		public int ProductoId { get; set; }

		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:C}")]
		[Column(TypeName = "decimal(10, 2)")]
		[Display(Name = "Precio unitario")]
		[Required(ErrorMessage = "Acordar y fijar el precio con el proveedor.")]
		public decimal PrecioUnitario { get; set; }

		public int Cantidad { get; set; }

		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:C}")]
		[Column(TypeName = "decimal(10, 2)")]
		[Display(Name = "Precio total")]
		public decimal PrecioTotal => (decimal) PrecioUnitario * Cantidad;

		[EnumDataType(typeof(EstadoOrdenCompra))]
		public EstadoOrdenCompra Estado { get; set; }
	}
}
