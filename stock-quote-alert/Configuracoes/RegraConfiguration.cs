using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stock_quote_alert_core.Interfaces;
using stock_quote_alert_core.Models.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Configuracoes
{
    public static class RegraConfiguration
    {
        public static void ConfiguraRegras(this IServiceCollection services, IConfiguration configuration)
        {
            string elementName = $"stock_quote_alert_core.Services.Executa{configuration.GetSection("ConfiguracaoServico").Get<ConfiguracaoServico>().TipoEnvio}, stock-quote-alert-core";

            Type elementType = Type.GetType(elementName);
            services.AddSingleton(typeof(IExecucao), elementType);
        }
    }
}
