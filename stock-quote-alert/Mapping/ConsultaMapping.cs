using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using stock_quote_alert.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Mapping
{
    class ConsultaMapping : IEntityTypeConfiguration<Consultas>
    {
        public void Configure(EntityTypeBuilder<Consultas> builder)
        {
            builder.ToTable("Consultas");

            builder.HasKey(c => c.IdConsulta);

            builder.Property(c => c.IdConsulta).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Email)
                   .WithOne(c => c.Consulta)
                   .HasForeignKey<EnvioEmail>(c => c.IdConsulta)
                   .IsRequired(false);

        }
    }
}
