using Microsoft.EntityFrameworkCore;
using stock_quote_alert.Mapping;
using stock_quote_alert.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Contexto
{
    public class AcaoContext : DbContext
    {
        public AcaoContext(DbContextOptions<AcaoContext> options) : base (options) 
        {

        }

        public DbSet<Consultas> Consultas{ get; set; }
        public DbSet<EnvioEmail> EnvioEmail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Consultas>(new ConsultaMapping().Configure);
            modelBuilder.Entity<EnvioEmail>(new EnvioEmailMaping().Configure);
        }
    }
}
