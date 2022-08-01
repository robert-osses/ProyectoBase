using Microsoft.EntityFrameworkCore;
using Sicade.Entidades.Auth;
using Sicade.Datos.Mapping.Auth;
using Sicade.Entidades.ETL;
using Sicade.Datos.Mapping.ETL;

namespace Sicade.Datos
{
    public class DbContextAuth : DbContext
    {
        public DbSet<rol> Rol { get; set; }
        public DbSet<nivel> Nivel { get; set; }
        public DbSet<persona_rol_unidad> Persona_rol_unidad { get; set; }
        public DbSet<auditor> Auditor { get; set; }

        //ETL
        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<personaETL> Persona { get; set; }
        public DbSet<estructura_superior> EstructuraSuperior { get; set; }

        public DbContextAuth(DbContextOptions<DbContextAuth> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new NivelMap());
            modelBuilder.ApplyConfiguration(new Persona_rol_unidadMap());
            modelBuilder.ApplyConfiguration(new AuditorMap());

            //ETL
            modelBuilder.ApplyConfiguration(new UnidadETLMAP());
            modelBuilder.ApplyConfiguration(new PersonaETLMAP());
            modelBuilder.ApplyConfiguration(new EstructuraSuperiorMap());
        }
    }    
}
