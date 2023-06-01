using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PNT1_Grupo6.Models;
namespace PNT1_Grupo6.Context
{
	public class EscuelaDatabaseContext : DbContext
	{
		public
	   EscuelaDatabaseContext(DbContextOptions<EscuelaDatabaseContext> options)
	   : base(options)
		{
		}
		public DbSet<Estudiante> Estudiantes { get; set; }
	}
}
