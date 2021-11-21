using Microsoft.Extensions.DependencyInjection;
using stock_quote_alert.Repositorios;
using stock_quote_alert_core.Interfaces;
using stock_quote_alert_core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Configuracoes
{
    public static class InjecaoDependencia
    {
        public static void ConfiguraInjecao(this IServiceCollection services)
        {
            services.AddSingleton<ICotacaoAPIService, CotacaoAPIService>();
            services.AddSingleton<IEmailService, EmailService>();

            services.AddSingleton<IConsultaRepositorio, ConsultasRepositorio>();
            services.AddSingleton<IEmailRepositorio, EnvioEmailRepositorio>();
        }

    }
}
