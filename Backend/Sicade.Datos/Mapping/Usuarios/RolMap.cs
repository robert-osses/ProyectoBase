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
    class RolMap : IEntityTypeConfiguration<rol>
    {
        public void Configure(EntityTypeBuilder<rol> builder)
        {
            builder.ToTable("ROL").HasQueryFilter(r => r.ESTADO == 1).HasKey(r => r.ID);
        }
    }
}
