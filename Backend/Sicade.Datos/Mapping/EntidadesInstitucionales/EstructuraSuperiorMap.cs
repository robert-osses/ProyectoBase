using Sicade.Entidades.ETL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sicade.Datos.Mapping.ETL
{
    class EstructuraSuperiorMap : IEntityTypeConfiguration<estructura_superior>
    {
        public void Configure(EntityTypeBuilder<estructura_superior> builder)
        {
            builder.ToTable("ESTRUCTURA_SUPERIOR");
        }
    }
}
