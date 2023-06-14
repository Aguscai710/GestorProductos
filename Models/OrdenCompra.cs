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

		[Display(Name = "Número de orden")]
		public string NumeroOrden { get; set; }

		[Display(Name = "Código del proveedor")]
		public string CodigoProveedor { get; set; }

		[Display(Name = "Proveedor")]
		public string NombreProveedor { get; set; }


		[Display(Name = "Código del producto")]
		public string CodigoProducto { get; set; }

		[Display(Name = "Producto")]
		public string NombreProducto{ get; set; }


		[Display(Name = "Precio unitario")]
		public double PrecioUnitario { get; set; }
		public int Cantidad { get; set; }

		[Display(Name = "Precio total")]
		public double PrecioTotal => PrecioUnitario * Cantidad;

		[EnumDataType(typeof(EstadoOrdenCompra))]
		public EstadoOrdenCompra Estado { get; set; }
	}
}
