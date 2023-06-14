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
		public string Nombre { get; set; }
		public string Apellido { get; set; }

		public int Dni { get; set; }

		[Display(Name = "Contraseña")]
		public string Password { get; set; }

		[EnumDataType(typeof(Rol))]
		public Rol RolUsuario { get; set; }

	}
}



