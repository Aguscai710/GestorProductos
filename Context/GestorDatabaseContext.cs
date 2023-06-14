using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PNT1_Grupo6.Models;
namespace PNT1_Grupo6.Context
{
	public class GestorDatabaseContext : DbContext
	{
		public
	   GestorDatabaseContext(DbContextOptions<GestorDatabaseContext> options)
	   : base(options)
		{
		}
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Proveedor> Proveedores { get; set; }

		public DbSet<Producto> Productos { get; set; }

		public DbSet<OrdenCompra> OrdenesCompra { get; set; }
	}
}

