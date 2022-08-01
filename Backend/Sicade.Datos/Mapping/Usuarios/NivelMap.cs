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
    class NivelMap : IEntityTypeConfiguration<nivel>
    {
        public void Configure(EntityTypeBuilder<nivel> builder)
        {
            builder.ToTable("NIVEL").HasQueryFilter(n => n.ESTADO == 1).HasKey(n => n.ID);
        }
    }
}
