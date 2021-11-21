using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stock_quote_alert_core.Models.Configuracoes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Configuracoes
{
    public static class ArgsConfiguration
    {
        private static ArgsModel criaModelByArgs(string[] args)
        {
            return new ArgsModel
            {
                Acao = args[0],
                PrecoMinimo = double.Parse(args[1], CultureInfo.InvariantCulture),
                PrecoMaximo = double.Parse(args[2], CultureInfo.InvariantCulture)
            };
        }

        private static ArgsModel criaModelByParamtros(IConfiguration configuration)
        {
            return new ArgsModel
            {
                Acao = configuration.GetValue<string>("NomeAcao"),
                PrecoMinimo = configuration.GetValue<double>("PrecoMinimo"),
                PrecoMaximo = configuration.GetValue<double>("PrecoMaximo"),
            };
        }


        
        public static void ConfiguraArgs(this IServiceCollection services, IConfiguration configuration, string [] args)
        {
            var argsViewModel = new ArgsModel();

            try
            {
                if (args.Length >= 3)
                {
                    argsViewModel = criaModelByArgs(args);
                }
                else
                {
                    argsViewModel = criaModelByParamtros(configuration);
                }
            }
            catch (Exception)
            {
                argsViewModel = criaModelByParamtros(configuration);
            }

            services.AddSingleton(argsViewModel);
        }
    }
}
