using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stock_quote_alert_core.Models.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Configuracoes
{
    public static class ParametrosConfiguration
    {
        public static void ConfiguraParametros(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiConfiguration>(configuration.GetSection("ApiConfiguration"));
            services.Configure<SMTPConfiguracao>(configuration.GetSection("ConfiguracaoEmail"));
            services.Configure<EmailDestino>(configuration.GetSection("EmailDestino"));
            services.Configure<ConfiguracaoServico>(configuration.GetSection("ConfiguracaoServico"));
        }
    }
}
