using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace PNT1_Grupo6.Models
{
	public class Usuario
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre  requerido")]
		[Display(Name = "Usuario")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "El apellido es requerido")]
		[Display(Name = "Apellido")]
		public string Apellido { get; set; }

		[Required(ErrorMessage = "El DNI es requerido")]
		public int Dni { get; set; }

		[Display(Name = "Contraseña")]
		[Required(ErrorMessage = "La contraseña es requerido")]
		public string Password { get; set; }

		[Required(ErrorMessage = "El Rol es requerido")]
		[EnumDataType(typeof(Rol))]
		public Rol RolUsuario { get; set; }

	}
}



