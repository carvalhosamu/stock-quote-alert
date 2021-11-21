using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_repositorio.Contexto
{
    class ContextFactory : IDesignTimeDbContextFactory<AcaoContext>
    {
        public AcaoContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=Acoes.db";
            var optionsBuilder = new DbContextOptionsBuilder<AcaoContext>();
            optionsBuilder.UseSqlite(connectionString);
            return new AcaoContext(optionsBuilder.Options);

        }
    }
}
