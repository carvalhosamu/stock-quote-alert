using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stock_quote_alert_repositorio.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Configuracoes
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string conexao = configuration.GetValue<string>("ConnectionString");

            services.AddDbContext<AcaoContext>(options =>
                options.UseSqlite(conexao));
        }
    }
}
