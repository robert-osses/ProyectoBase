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
    class AuditorMap : IEntityTypeConfiguration<auditor>
    {
        public void Configure(EntityTypeBuilder<auditor> builder)
        {
            builder.ToTable("AUDITOR").HasKey(a => a.ID);
        }
    }
}
