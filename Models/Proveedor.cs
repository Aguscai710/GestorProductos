using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PNT1_Grupo6.Models
{
	public class Proveedor
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Display(Name = "Código Proveedor")]
		public string CodigoProveedor { get; set; }
		[Display(Name = "Proveedor")]
		public string Nombre { get; set; }
		public string Email { get; set; }
		public string Telefono { get; set; }
		public string Rubro { get; set; }
	}
}