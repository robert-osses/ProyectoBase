using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sicade.Entidades.ETL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sicade.Datos.Mapping.ETL
{
    class UnidadETLMAP : IEntityTypeConfiguration<Unidad>
    {
        public void Configure(EntityTypeBuilder<Unidad> builder)
        {
            builder.ToTable("UNIDAD");
        }
    }
}
