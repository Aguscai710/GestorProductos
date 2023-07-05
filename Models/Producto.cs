using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PNT1_Grupo6.Models
{
	public class Producto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Código Producto")]
		public int Id { get; set; }
		[Display(Name = "Producto")]
		[Required(ErrorMessage = "Se requiere el nombre del producto")]
		public string Nombre { get; set; }
		[Required(ErrorMessage = "El stock inicial no debe superar al máximo almacenable.")]
		public int Stock { get; set; }
		[Required(ErrorMessage = "Defina un máximo almacenable.")]
		public int MaximoAlmacenable { get; set; }
		[Required(ErrorMessage = "Añadir detalle para clarificar info del producto.")]
		public string Descripcion { get; set; }

		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:C}")]
		[Column(TypeName = "decimal(10, 2)")]
		[Required(ErrorMessage = "Establezca un precio.")]
		public decimal PrecioVenta { get; set; }
	}
}
