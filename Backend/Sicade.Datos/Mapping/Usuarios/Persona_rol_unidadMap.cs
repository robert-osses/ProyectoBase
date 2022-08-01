using Sicade.Entidades.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicade.Datos.Mapping.Auth
{
    class Persona_rol_unidadMap : IEntityTypeConfiguration<persona_rol_unidad>
    {
        public void Configure(EntityTypeBuilder<persona_rol_unidad> builder)
        {
            builder.ToTable("PERSONA_ROL_UNIDAD").HasKey(p => p.ID);
        }
    }
}
