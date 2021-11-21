using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using stock_quote_alert_core.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_repositorio.Mapping
{
    public class EnvioEmailMaping : IEntityTypeConfiguration<EnvioEmail>
    {
        public void Configure(EntityTypeBuilder<EnvioEmail> builder)
        {
            builder.ToTable("EnvioEmail");

            builder.HasKey(c => c.IdEmail);

            builder.Property(c => c.TipoEnvio)
                   .HasMaxLength(1);

            builder.HasOne(c => c.Consulta)
            .WithOne(c => c.Email)
            .HasForeignKey<EnvioEmail>(c => c.IdConsulta)
            .IsRequired(false);
        }
    }
}
