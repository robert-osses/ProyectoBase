using Sicade.Entidades.ETL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sicade.Datos.Mapping.ETL
{
    class PersonaETLMAP : IEntityTypeConfiguration<personaETL>
    {
        public void Configure(EntityTypeBuilder<personaETL> builder)
        {
            builder.ToTable("PERSONA");
        }
    }
}
