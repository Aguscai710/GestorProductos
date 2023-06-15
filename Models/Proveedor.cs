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

		[Required(ErrorMessage = "El código de proveedor es requerido.")]
		public string CodigoProveedor { get; set; }

		[Required(ErrorMessage = "El proveedor es requerido.")]
		[Display(Name = "Proveedor")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "El mail de contacto es requerido.")]
		[EmailAddress(ErrorMessage = "El campo Email no tiene un formato válido.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "El teléfono de contacto es requerido.")]
		public string Telefono { get; set; }

		[Required(ErrorMessage = "Se requiere especificar el rubro del proveedor.")]
		public string Rubro { get; set; }
	}
}