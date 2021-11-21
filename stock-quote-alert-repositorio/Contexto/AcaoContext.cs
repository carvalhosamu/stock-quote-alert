using Microsoft.EntityFrameworkCore;
using stock_quote_alert_core.Models.Tabelas;
using stock_quote_alert_repositorio.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_repositorio.Contexto
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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Consultas>(new ConsultaMapping().Configure);
            modelBuilder.Entity<EnvioEmail>(new EnvioEmailMaping().Configure);
        }
    }
}
