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
		public string CodigoProducto { get; set; }
		[Display(Name = "Producto")]
		public string Nombre { get; set; }
		public int Stock { get; set; }
		public int MaximoAlmacenable { get; set; }
		public string Descripcion { get; set; }
		public double PrecioVenta { get; set; }
	}
}
